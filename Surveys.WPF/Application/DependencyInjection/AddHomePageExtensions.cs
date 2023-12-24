using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Application.DependencyInjection
{
    public static class AddHomePageExtensions
    {
        public static IHostBuilder AddHomePage(this IHostBuilder host)
        {
            host.ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddTransient<HomeViewModel>(
                    (services) => HomeViewModel.LoadViewModel(
                        services.GetRequiredService<AuthenticationStore>(),
                   
                  
                     //   services.GetRequiredService<NavigationService<ProfileViewModel>>(),
                        services.GetRequiredService<NavigationService<LoginViewModel>>()));

                serviceCollection.AddNavigationService<HomeViewModel>();
            });

            return host;
        }
    }
}
