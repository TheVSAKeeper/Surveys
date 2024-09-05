using System.Security.Claims;
using Calabonga.Microservices.Core;
using Microsoft.AspNetCore.Authorization;

namespace Surveys.Web.Definitions.OpenIddict;

/// <summary>
///     Permission handler for custom authorization implementations
/// </summary>
public class AppPermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User.Identity is null)
        {
            return Task.CompletedTask;
        }

        ClaimsIdentity? identity = context.User.Identity as ClaimsIdentity;
        string? claim = ClaimsHelper.GetValue<string>(identity, requirement.PermissionName);

        if (claim == null)
        {
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
