using System.Windows;
using System.Windows.Controls;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Features.Authentication.Register;

public class RegisterCommand : AsyncCommandBase
{
    private readonly AuthenticationStore _authenticationStore;
    private readonly INavigationService _loginNavigationService;
    private readonly RegisterFormViewModel _registerViewModel;

    public RegisterCommand(
        RegisterFormViewModel registerViewModel,
        AuthenticationStore authenticationStore,
        INavigationService loginNavigationService)
    {
        _registerViewModel = registerViewModel;
        _authenticationStore = authenticationStore;
        _loginNavigationService = loginNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter)
    {
        /*string password = _registerViewModel.Password;
        string confirmPassword = _registerViewModel.ConfirmPassword;

        if (password != confirmPassword)
        {
            MessageBox.Show("Password and confirm password must match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }*/

        try
        {
            PasswordBox box = (PasswordBox)parameter;
            
            await _authenticationStore.CreateUserAsync(_registerViewModel.Username, box.Password, _registerViewModel.Role);

            MessageBox.Show("Successfully registered!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _loginNavigationService.Navigate();
        }
        catch (Exception)
        {
            MessageBox.Show("Registration failed. Please check your information or try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}