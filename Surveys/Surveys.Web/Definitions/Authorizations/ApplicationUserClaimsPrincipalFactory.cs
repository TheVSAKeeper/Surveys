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

        if (user.ApplicationUserProfile?.Permissions != null)
        {
            List<AppPermission> permissions = user.ApplicationUserProfile.Permissions.ToList();

            if (permissions.Count != 0)
                permissions.ForEach(x => ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(x.PolicyName, nameof(x.PolicyName).ToLower())));
        }

        ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim("framework", "nimble"));

        if (!string.IsNullOrWhiteSpace(user.UserName))
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.Name, user.UserName));

        if (!string.IsNullOrWhiteSpace(user.FirstName))
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));

        if (!string.IsNullOrWhiteSpace(user.LastName))
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

        return principal;
    }
}