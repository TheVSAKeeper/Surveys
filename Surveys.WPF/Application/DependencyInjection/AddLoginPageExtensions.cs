using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Application.DependencyInjection;

public static class AddLoginPageExtensions
{
    public static IHostBuilder AddLoginPage(this IHostBuilder host)
    {
        host.ConfigureServices(serviceCollection =>
        {
            serviceCollection.AddTransient<LoginViewModel>(services => new LoginViewModel(services.GetRequiredService<AuthenticationStore>(),
                services.GetRequiredService<NavigationService<HomeViewModel>>()));

            serviceCollection.AddNavigationService<LoginViewModel>();
        });

        return host;
    }
}