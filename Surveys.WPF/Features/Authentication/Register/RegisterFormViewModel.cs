using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Surveys.Infrastructure;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.Register;

public class RegisterFormViewModel : ViewModelBase
{
    private string _confirmPassword;
    /*private string _firstName;
    private string _lastName;
    private string? _patronymic;*/
    private string _password;
    private string _role;
    private string _username;

    public RegisterFormViewModel(AuthenticationStore authenticationStore, ApplicationRoleStore roleStore, INavigationService loginNavigationService)
    {
        Roles = roleStore.Roles.Select(role => role.Name).ToList();
        SubmitCommand = new RegisterCommand(this, authenticationStore, loginNavigationService);
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
    }

    public List<string?> Roles { get; }
    public string Username
    {
        get => _username;
        set => Set(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => Set(ref _password, value);
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => Set(ref _confirmPassword, value);
    }

    /*public string LastName
    {
        get => _lastName;
        set => Set(ref _lastName, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => Set(ref _firstName, value);
    }

    public string? Patronymic
    {
        get => _patronymic;
        set => Set(ref _patronymic, value);
    }*/
    
    public string Role
    {
        get => _role;
        set => Set(ref _role, value);
    }

    public ICommand SubmitCommand { get; }

    public ICommand NavigateLoginCommand { get; }
}