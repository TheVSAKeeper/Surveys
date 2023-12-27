using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Definitions.Initialization;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Definitions.ViewModels;

public class MainWindowDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddSingleton<ApplicationInitializer>();

        services.AddSingleton<MainViewModel>();

        services.AddSingleton<MainWindow>(serviceProvider => new MainWindow
        {
            DataContext = serviceProvider.GetRequiredService<MainViewModel>()
        });
    }
}