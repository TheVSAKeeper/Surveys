using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Login;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Login;

public class LoginViewModel(AuthenticationStore authenticationStore, INavigationService homeNavigationService)
    : ViewModelBase
{
    public LoginFormViewModel LoginFormViewModel { get; } = new(authenticationStore, homeNavigationService);
}