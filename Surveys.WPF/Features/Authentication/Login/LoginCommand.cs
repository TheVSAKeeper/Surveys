using System.Windows;
using System.Windows.Controls;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Features.Authentication.Login;

public class LoginCommand : AsyncCommandBase
{
    private readonly AuthenticationStore _authenticationStore;
    private readonly INavigationService _homeNavigationService;
    private readonly LoginFormViewModel _loginViewModel;

    public LoginCommand(
        LoginFormViewModel loginViewModel,
        AuthenticationStore authenticationStore,
        INavigationService homeNavigationService)
    {
        _loginViewModel = loginViewModel;
        _authenticationStore = authenticationStore;
        _homeNavigationService = homeNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            PasswordBox box = (PasswordBox)parameter;

            await _authenticationStore.SignInAsync(_loginViewModel.Email, box.Password);

            MessageBox.Show("Successfully logged in!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _homeNavigationService.Navigate();
        }
        catch (Exception)
        {
            MessageBox.Show("Login failed. Please check your information or try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}