using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.Infrastructure;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Pages.Register;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Application.DependencyInjection;

public static class AddRegisterPageExtensions
{
    public static IHostBuilder AddRegisterPage(this IHostBuilder host)
    {
        host.ConfigureServices(serviceCollection =>
        {
            serviceCollection.AddTransient<RegisterViewModel>(services => new RegisterViewModel(
                services.GetRequiredService<AuthenticationStore>(),
                services.GetRequiredService<ApplicationRoleStore>(),
                services.GetRequiredService<NavigationService<LoginViewModel>>()));

            serviceCollection.AddNavigationService<RegisterViewModel>();
        });

        return host;
    }
}