using Calabonga.AspNetCore.AppDefinitions;
using Surveys.Web.Application.Services;
using Surveys.Web.Definitions.Authorizations;

namespace Surveys.Web.Definitions.DependencyContainer;

/// <summary>
///     Dependency container definition
/// </summary>
public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<ApplicationUserClaimsPrincipalFactory>();
    }
}