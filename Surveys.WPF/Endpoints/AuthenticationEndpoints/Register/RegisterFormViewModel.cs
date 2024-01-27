using System.Windows.Input;
using Surveys.Infrastructure;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.AuthenticationEndpoints.Register;

public class RegisterFormViewModel : ViewModelBase
{
    private string? _role;
    private string? _username;

    public RegisterFormViewModel(AuthenticationManager authenticationManager, ApplicationRoleStore roleStore)
    {
        Roles = roleStore.Roles.Select(role => role.Name).ToList();
        SubmitCommand = new RegisterCommand(this, authenticationManager);
    }

    public List<string?> Roles { get; }

    public string? Username
    {
        get => _username;
        set => Set(ref _username, value);
    }

    public string? Role
    {
        get => _role;
        set => Set(ref _role, value);
    }

    public ICommand SubmitCommand { get; }
}