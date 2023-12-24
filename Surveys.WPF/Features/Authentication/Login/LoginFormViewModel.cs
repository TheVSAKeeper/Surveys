using System.Windows.Input;
using System.Windows.Markup;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.Login;

[MarkupExtensionReturnType(typeof(LoginFormViewModel))]
public class LoginFormViewModel : ViewModelBase
{
    private string _email;

    public LoginFormViewModel(
        AuthenticationStore authenticationStore,
        INavigationService registerNavigationService,
        INavigationService homeNavigationService
    )
    {
        SubmitCommand = new LoginCommand(this, authenticationStore, homeNavigationService);
        NavigateRegisterCommand = new NavigateCommand(registerNavigationService);
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

    public ICommand SubmitCommand { get; }

    public ICommand NavigateRegisterCommand { get; }
}