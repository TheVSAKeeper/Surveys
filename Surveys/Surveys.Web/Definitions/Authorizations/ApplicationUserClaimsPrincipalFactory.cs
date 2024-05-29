using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Surveys.Infrastructure;

namespace Surveys.Web.Definitions.Authorizations;

/// <summary>
///     User Claims Principal Factory override from Microsoft Identity framework
/// </summary>
public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    /// <inheritdoc />
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    /// <summary>
    ///     Creates a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> from an user asynchronously.
    /// </summary>
    /// <param name="user">The user to create a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> from.</param>
    /// <returns>
    ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous creation operation,
    ///     containing the created <see cref="T:System.Security.Claims.ClaimsPrincipal" />.
    /// </returns>
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        ClaimsPrincipal principal = await base.CreateAsync(user);
        ClaimsIdentity identity = (ClaimsIdentity)principal.Identity!;

        AddPermissionClaims(user, identity);
        AddUserClaims(user, identity);

        return principal;
    }

    private static void AddPermissionClaims(ApplicationUser user, ClaimsIdentity identity)
    {
        if (user.ApplicationUserProfile?.Permissions == null || user.ApplicationUserProfile.Permissions.Count == 0)
            return;

        List<AppPermission> permissions = user.ApplicationUserProfile.Permissions.ToList();
        permissions.ForEach(permission => identity.AddClaim(new Claim(permission.PolicyName, nameof(permission.PolicyName).ToLower())));
    }

    private static void AddUserClaims(ApplicationUser user, ClaimsIdentity identity)
    {
        if (string.IsNullOrWhiteSpace(user.UserName) == false)
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

        if (string.IsNullOrWhiteSpace(user.FirstName) == false)
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));

        if (string.IsNullOrWhiteSpace(user.LastName) == false)
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

        if (string.IsNullOrWhiteSpace(user.Patronymic) == false)
            identity.AddClaim(new Claim(nameof(user.Patronymic).ToLower(), user.Patronymic));
    }
}