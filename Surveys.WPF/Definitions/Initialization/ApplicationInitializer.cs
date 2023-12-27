using System.Windows;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Definitions.Initialization;

public class ApplicationInitializer(
    Features.Authentication.AuthenticationStore authenticationStore,
    NavigationService<HomeViewModel> homeNavigationService,
    NavigationService<LoginViewModel> loginNavigationService)
{
    public async Task Initialize()
    {
        try
        {
            await authenticationStore.Initialize();

            if (authenticationStore.IsLoggedIn)
                homeNavigationService.Navigate();
            else
                loginNavigationService.Navigate();
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}