using System.Windows.Input;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.AuthenticationEndpoints.Login;

public class LoginFormViewModel : ViewModelBase
{
    private string? _username;

    public LoginFormViewModel(
        AuthenticationManager authenticationManager,
        INavigationService homeNavigationService
    )
    {
        SubmitCommand = new LoginCommand(this, authenticationManager, homeNavigationService);
    }

    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public ICommand SubmitCommand { get; }
}