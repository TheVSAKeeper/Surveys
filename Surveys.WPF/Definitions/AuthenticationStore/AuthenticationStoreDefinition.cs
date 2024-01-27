using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Endpoints.AuthenticationEndpoints;

namespace Surveys.WPF.Definitions.AuthenticationStore;

public class AuthenticationStoreDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddSingleton<AuthenticationManager>();
    }
}