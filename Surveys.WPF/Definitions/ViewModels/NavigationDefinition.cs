using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.Navigation.Modal;

namespace Surveys.WPF.Definitions.ViewModels;

public class NavigationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();
        services.AddSingleton<CloseModalNavigationService>();
    }
}