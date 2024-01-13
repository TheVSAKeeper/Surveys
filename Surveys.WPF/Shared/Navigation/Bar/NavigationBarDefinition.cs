using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Shared.Navigation.Bar;

public class NavigationBarDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddSingleton<NavigationBarViewModel>();
    }
}