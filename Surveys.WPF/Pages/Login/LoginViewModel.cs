using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Login;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Login;

public class LoginViewModel(AuthenticationStore authenticationStore, NavigationService<HomeViewModel> homeNavigationService)
    : ViewModelBase
{
    public LoginFormViewModel LoginFormViewModel { get; } = new(authenticationStore, homeNavigationService);
}