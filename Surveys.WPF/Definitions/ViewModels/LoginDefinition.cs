using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Definitions.ViewModels;

public class LoginDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<LoginViewModel>();

        services.AddNavigationService<LoginViewModel>();
    }
}