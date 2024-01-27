using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.Identity;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.AuthenticationEndpoints.Login;

public class LoginCommand : AsyncCommandBase
{
    private readonly AuthenticationManager _authenticationManager;
    private readonly INavigationService _homeNavigationService;
    private readonly LoginFormViewModel _loginViewModel;

    public LoginCommand(
        LoginFormViewModel loginViewModel,
        AuthenticationManager authenticationManager,
        INavigationService homeNavigationService)
    {
        _loginViewModel = loginViewModel;
        _authenticationManager = authenticationManager;
        _homeNavigationService = homeNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            if (parameter is not PasswordBox passwordBox)
                return;

            SignInResult result = await _authenticationManager.SignInAsync(_loginViewModel.Username!, passwordBox.Password);

            if (result.Succeeded == false)
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _homeNavigationService.Navigate();
        }
        catch (Exception)
        {
            MessageBox.Show("Ошибка входа. Пожалуйста, проверьте вашу информацию или повторите попытку позже.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override bool CanExecuteAsync(object? parameter) => _loginViewModel.Username != null;
}