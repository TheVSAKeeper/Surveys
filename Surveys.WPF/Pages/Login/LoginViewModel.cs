using System.Windows.Markup;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Login;
using Surveys.WPF.Pages.Register;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Login;

[MarkupExtensionReturnType(typeof(LoginViewModel))]
public class LoginViewModel : ViewModelBase
{
    public LoginViewModel(
        AuthenticationStore authenticationStore,
        NavigationService<RegisterViewModel> registerNavigationService,
        INavigationService homeNavigationService
    )
    {
        LoginFormViewModel = new LoginFormViewModel(authenticationStore,
            registerNavigationService,
            homeNavigationService);
    }

    public LoginFormViewModel LoginFormViewModel { get; }
}