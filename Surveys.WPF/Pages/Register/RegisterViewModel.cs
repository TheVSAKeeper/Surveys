using Surveys.Infrastructure;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Register;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Register;

public class RegisterViewModel : ViewModelBase
{
    public RegisterViewModel(AuthenticationStore authenticationStore,ApplicationRoleStore roleStore, INavigationService loginNavigationService)
    {
        RegisterFormViewModel = new RegisterFormViewModel(authenticationStore,roleStore, loginNavigationService);
    }

    public RegisterFormViewModel RegisterFormViewModel { get; }
}