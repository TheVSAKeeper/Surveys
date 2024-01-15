using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation.Modal;

namespace Surveys.WPF.Shared.Navigation;

public class NavigationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddSingleton<NavigationMediator>();
        services.AddSingleton<ModalNavigationMediator>();
        services.AddSingleton<CloseModalNavigationService>();
    }
}