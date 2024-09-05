using OpenIddict.Abstractions;
using Surveys.Infrastructure;

namespace Surveys.Web.HostedServices;

public sealed class OpenIddictWorker(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        IOpenIddictApplicationManager manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("blazor-client", cancellationToken) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "blazor-client",
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                DisplayName = "Blazor client application",
                ClientType = OpenIddictConstants.ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:5001/authentication/logout-callback"),
                    new Uri("https://localhost:5001/logout-oidc"),
                    new Uri("https://localhost:5001/signout-callback-oidc")
                },
                RedirectUris =
                {
                    new Uri("https://localhost:5001/authentication/login-callback"),
                    new Uri("https://localhost:5001/signin-oidc")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                    OpenIddictConstants.Permissions.ResponseTypes.Code,

                    OpenIddictConstants.Permissions.Prefixes.Scope,

                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles,

                    OpenIddictConstants.Permissions.ResponseTypes.IdToken
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                }
            }, cancellationToken);
        }

        // credentials password
        const string client_id1 = "client_id1";

        if (await manager.FindByClientIdAsync(client_id1, cancellationToken) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = client_id1,
                ClientSecret = "client-secret-sts",
                DisplayName = "Service-To-Service demonstration",
                Permissions =
                {
                    // Endpoint permissions
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    // Grant type permissions
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    // Scope permissions
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                }
            }, cancellationToken);
        }

        const string client_id2 = "client-id-code";

        if (await manager.FindByClientIdAsync(client_id2, cancellationToken) is null)
        {
            string? url = serviceProvider.GetRequiredService<IConfiguration>().GetValue<string>("AuthServer:Url");

            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = client_id2,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                ClientSecret = "client-secret-code",
                DisplayName = "API testing clients with Authorization Code Flow demonstration",
                RedirectUris =
                {
                    new Uri("https://www.thunderclient.com/oauth/callback"), // https://www.thunderclient.com/
                    new Uri($"{url}/swagger/oauth2-redirect.html"), // https://swagger.io/
                    new Uri("https://localhost:20001/swagger/oauth2-redirect.html") // https://swagger.io/ for Module as Example
                },
                Permissions =
                {
                    // Endpoint permissions
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Revocation,

                    // Grant type permissions
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                    // Scope permissions
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api",
                    OpenIddictConstants.Permissions.Prefixes.Scope + "custom",

                    // Response types
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.ResponseTypes.IdToken
                }
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
