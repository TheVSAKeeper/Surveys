using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Application.Initialization;
using Surveys.WPF.Shared.ViewModels;
using Surveys.WPF.ViewModels;
using Surveys.WPF.Views.Windows;

namespace Surveys.WPF.Application.DependencyInjection;

public static class AddMainWindowExtensions
{
    public static IHostBuilder AddMainWindow(this IHostBuilder host)
    {
        host.ConfigureServices(serviceCollection =>
        {
            serviceCollection.AddSingleton<ApplicationInitializer>();

            serviceCollection.AddSingleton<MainViewModel>();

            serviceCollection.AddSingleton<MainWindow>(services => new MainWindow
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            });
        });

        return host;
    }
}