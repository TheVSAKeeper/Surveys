using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Features.Authentication;

namespace Surveys.WPF.Application.DependencyInjection;

public class AuthenticationStoreDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddSingleton<AuthenticationStore>();
    }
}