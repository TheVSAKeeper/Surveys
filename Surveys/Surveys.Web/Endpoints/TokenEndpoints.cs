using System.Security.Claims;
using Calabonga.Microservices.Core.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Surveys.Infrastructure;
using Surveys.Web.Application.Services;

namespace Surveys.Web.Endpoints;

/// <summary>
///     Token Endpoint for OpenIddict
/// </summary>
public sealed class TokenEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapPost("~/connect/token", async (
                HttpContext httpContext,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IAccountService accountService) =>
            {
                OpenIddictRequest request = httpContext.GetOpenIddictServerRequest()
                                            ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

                if (request.IsClientCredentialsGrantType())
                {
                    ClaimsIdentity identity = new(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                    // Subject or sub is a required field, we use the client id as the subject identifier here.
                    identity.AddClaim(OpenIddictConstants.Claims.Subject, request.ClientId!);
                    identity.AddClaim(OpenIddictConstants.Claims.ClientId, request.ClientId!);

                    // Don't forget to add destination otherwise it won't be added to the access token.
                    if (request.Scope.IsNullOrEmpty())
                    {
                        identity.AddClaim(OpenIddictConstants.Claims.Scope, request.Scope!, OpenIddictConstants.Destinations.AccessToken);
                    }

                    identity.AddClaim("nimble", "framework", OpenIddictConstants.Destinations.AccessToken);

                    ClaimsPrincipal claimsPrincipal = new(identity);

                    claimsPrincipal.SetScopes(request.GetScopes());
                    return Results.SignIn(claimsPrincipal, new AuthenticationProperties(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                if (request.IsPasswordGrantType())
                {
                    if (request.Username != null)
                    {
                        ApplicationUser? user = await userManager.FindByNameAsync(request.Username);

                        if (user == null)
                        {
                            return Results.Problem("Invalid operation: user not found");
                        }

                        // Ensure the user is allowed to sign in
                        if (await signInManager.CanSignInAsync(user) == false)
                        {
                            return Results.Problem("Invalid operation: user is not allowed to sign in");
                        }

                        // Ensure the user is not already locked out
                        if (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
                        {
                            return Results.Problem("Invalid operation: user is already locked out");
                        }

                        // Ensure the password is valid
                        if (request.Password != null && await userManager.CheckPasswordAsync(user, request.Password) == false)
                        {
                            if (userManager.SupportsUserLockout)
                            {
                                await userManager.AccessFailedAsync(user);
                            }

                            return Results.Problem("Invalid operation: password is not valid");
                        }

                        // Reset the lockout count
                        if (userManager.SupportsUserLockout)
                        {
                            await userManager.ResetAccessFailedCountAsync(user);
                        }

                        ClaimsPrincipal principal = await accountService.GetPrincipalForUserAsync(user);
                        return Results.SignIn(principal, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                    }
                }

                if (request.IsAuthorizationCodeGrantType() == false)
                {
                    return Results.Problem("The specified grant type is not supported.");
                }

                {
                    AuthenticateResult authenticateResult = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                    ClaimsPrincipal? claimsPrincipal = authenticateResult.Principal;
                    return Results.SignIn(claimsPrincipal!, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }
            })
            .ExcludeFromDescription()
            .AllowAnonymous();
    }
}
