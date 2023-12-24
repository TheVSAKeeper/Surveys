using System.Windows.Input;
using System.Windows.Markup;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.Login;

[MarkupExtensionReturnType(typeof(LoginFormViewModel))]
public class LoginFormViewModel : ViewModelBase
{
    private string _email;

    private string _password;

    public LoginFormViewModel(
        AuthenticationStore authenticationStore,
        //  INavigationService registerNavigationService,
        INavigationService homeNavigationService
        //    , INavigationService passwordResetNavigationService
    )
    {
        SubmitCommand = new LoginCommand(this, authenticationStore, homeNavigationService);
        // NavigateRegisterCommand = new NavigateCommand(registerNavigationService);
        //  NavigatePasswordResetCommand = new NavigateCommand(passwordResetNavigationService);
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand SubmitCommand { get; }

//    public ICommand NavigateRegisterCommand { get; }

    //  public ICommand NavigatePasswordResetCommand { get; }
}