using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Profile;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Application.DependencyInjection;

public static class AddProfilePageExtensions
{
    public static IHostBuilder AddProfilePage(this IHostBuilder host)
    {
        host.ConfigureServices(serviceCollection =>
        {
            serviceCollection.AddTransient<ProfileViewModel>(services => new ProfileViewModel(services.GetRequiredService<AuthenticationStore>(),
                services.GetRequiredService<NavigationService<HomeViewModel>>()));

            serviceCollection.AddNavigationService<ProfileViewModel>();
        });

        return host;
    }
}