using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.Microservices.Core.Extensions;
using Calabonga.Microservices.Core.Validators;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using Surveys.Domain.Base;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.ProfileMessages.ViewModels;
using Surveys.Web.Definitions.Authorizations;

namespace Surveys.Web.Application.Services;

/// <summary>
///     Account service
/// </summary>
public class AccountService : IAccountService
{
    private readonly ApplicationUserClaimsPrincipalFactory _claimsFactory;
    private readonly IHttpContextAccessor _httpContext;
    private readonly ILogger<AccountService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountService(
        IUserStore<ApplicationUser> userStore,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<RoleManager<ApplicationRole>> loggerRole,
        IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
        IUnitOfWork<ApplicationDbContext> unitOfWork,
        ILogger<AccountService> logger,
        ILogger<UserManager<ApplicationUser>> loggerUser,
        ApplicationUserClaimsPrincipalFactory claimsFactory,
        IHttpContextAccessor httpContext,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _claimsFactory = claimsFactory;
        _httpContext = httpContext;
        _mapper = mapper;

        // We need to created a custom instance for current service
        // It'll help to use Transaction in the Unit Of Work
        _userManager = new UserManager<ApplicationUser>(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
            loggerUser);

        RoleStore<ApplicationRole, ApplicationDbContext, Guid> roleStore = new(_unitOfWork.DbContext);
        _roleManager = new RoleManager<ApplicationRole>(roleStore, roleValidators, keyNormalizer, errors, loggerRole);
    }

    /// <inheritdoc />
    public Guid GetCurrentUserId()
    {
        IIdentity? identity = _httpContext.HttpContext?.User.Identity;
        string? identitySub = identity?.GetSubjectId();
        return identitySub?.ToGuid() ?? Guid.Empty;
    }

    /// <summary>
    ///     Returns <see cref="ApplicationUser" /> instance after successful registration
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    public async Task<Operation<UserProfileViewModel, string>> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken)
    {
        ApplicationUser? user = _mapper.Map<ApplicationUser>(model);
        await using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync();
        IdentityResult result = await _userManager.CreateAsync(user!, model.Password);
        const string Role = AppData.DoctorRoleName;

        if (result.Succeeded)
        {
            if (await _roleManager.FindByNameAsync(Role) == null)
                return await Task.FromResult(Operation.Error(AppData.Exceptions.UserNotFoundException));

            await _userManager.AddToRoleAsync(user!, Role);

            ApplicationUserProfile? profile = _mapper.Map<ApplicationUserProfile>(model);
            IRepository<ApplicationUserProfile> profileRepository = _unitOfWork.GetRepository<ApplicationUserProfile>();

            await profileRepository.InsertAsync(profile!, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                ClaimsPrincipal principal = await _claimsFactory.CreateAsync(user!);
                UserProfileViewModel? mapped = _mapper.Map<UserProfileViewModel>(principal.Identity);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("User {@User} successfully created with {@Role}", model, Role);

                if (mapped is not null)
                    return Operation.Result(mapped);

                return Operation.Error(AppData.Exceptions.MappingException);
            }
        }

        IEnumerable<string> errors = result.Errors.Select(x => $"{x.Code}: {x.Description}");
        string errorMessage = string.Join(", ", errors);
        await transaction.RollbackAsync(cancellationToken);
        _logger.LogError("User {User} creation failed with {Errors}", model.Email, errorMessage);
        return await Task.FromResult(Operation.Error(errorMessage));
    }

    /// <summary>
    ///     Returns ClaimPrincipal by user identity
    /// </summary>
    /// <param name="identifier"></param>
    public async Task<ClaimsPrincipal> GetPrincipalByIdAsync(string identifier)
    {
        if (string.IsNullOrEmpty(identifier))
            throw new MicroserviceException();

        ApplicationUser? user = await _userManager.FindByIdAsync(identifier);

        if (user == null)
            throw new MicroserviceUserNotFoundException();

        ClaimsPrincipal defaultClaims = await _claimsFactory.CreateAsync(user);
        return defaultClaims;
    }

    /// <summary>
    ///     Returns ClaimPrincipal by user identity
    /// </summary>
    /// <param name="user"></param>
    public Task<ClaimsPrincipal> GetPrincipalForUserAsync(ApplicationUser user) => _claimsFactory.CreateAsync(user);

    /// <summary>
    ///     Returns user by his identifier
    /// </summary>
    /// <param name="id"></param>
    public Task<ApplicationUser?> GetByIdAsync(Guid id) => _userManager.FindByIdAsync(id.ToString());

    /// <summary>
    ///     Returns current user account information or null when user does not logged in
    /// </summary>
    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        string userId = GetCurrentUserId().ToString();
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        return user;
    }

    /// <summary>
    ///     Returns a collection of the <see cref="ApplicationUser" /> by emails
    /// </summary>
    /// <param name="emails"></param>
    public async Task<IEnumerable<ApplicationUser>> GetUsersByEmailsAsync(IEnumerable<string> emails)
    {
        List<ApplicationUser> result = [];

        foreach (string email in emails)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user != null && result.Contains(user) == false)
                result.Add(user);
        }

        return await Task.FromResult(result);
    }

    /// <summary>
    ///     Check roles for current user
    /// </summary>
    /// <param name="roleNames"></param>
    public async Task<PermissionValidationResult> IsInRolesAsync(string[] roleNames)
    {
        string userId = GetCurrentUserId().ToString();
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            PermissionValidationResult resultUserNotFound = new();
            resultUserNotFound.AddError(AppData.Exceptions.UnauthorizedException);
            return await Task.FromResult(resultUserNotFound);
        }

        foreach (string roleName in roleNames)
        {
            bool ok = await _userManager.IsInRoleAsync(user, roleName);

            if (ok)
                return new PermissionValidationResult();
        }

        PermissionValidationResult result = new();
        result.AddError(AppData.Exceptions.UnauthorizedException);
        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName) => await _userManager.GetUsersInRoleAsync(roleName);

    #region privates

    private async Task AddClaimsToUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
    {
        if (!string.IsNullOrEmpty(user.UserName))
            await userManager.AddClaimAsync(user, new Claim(OpenIddictConstants.Claims.Name, user.UserName));

        if (!string.IsNullOrEmpty(user.Email))
            await userManager.AddClaimAsync(user, new Claim(OpenIddictConstants.Claims.Email, user.Email));

        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.FirstName ?? "John"));
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName ?? "Doe"));
        await userManager.AddClaimAsync(user, new Claim(OpenIddictConstants.Claims.Role, role));
    }

    #endregion
}