using System.Windows.Markup;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Login;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Login
{
    [MarkupExtensionReturnType(typeof(LoginViewModel))]

    public class LoginViewModel : ViewModelBase
    {
        public LoginFormViewModel LoginFormViewModel { get; }

        public LoginViewModel(
            AuthenticationStore authenticationStore, 
           // INavigationService registerNavigationService, 
            INavigationService homeNavigationService
        //    , INavigationService passwordResetNavigationService
            )
        {
            LoginFormViewModel = new LoginFormViewModel(
                authenticationStore,
              //  registerNavigationService,
                homeNavigationService
              //  , passwordResetNavigationService
                );
        }
    }
}
