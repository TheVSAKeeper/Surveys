using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation.Bar;

namespace Surveys.WPF.Definitions.ViewModels;

public class NavigationBarDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddSingleton<NavigationBarViewModel>();
    }
}