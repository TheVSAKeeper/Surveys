using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.Identity;
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
        try
        {
            if (parameter is not PasswordBox passwordBox)
                return;

            IdentityResult result = await _authenticationStore.CreateUserAsync(_registerViewModel.Username, passwordBox.Password, _registerViewModel.Role);

            if (result.Succeeded == false)
            {
                string errors = string.Join(Environment.NewLine, result.Errors.Select(error => error.Description));
                MessageBox.Show($"Ошибка регистрации. {Environment.NewLine}{errors}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Успешно зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            _loginNavigationService.Navigate();
        }
        catch (Exception)
        {
            MessageBox.Show("Регистрация не удалась. Пожалуйста, проверьте вашу информацию или повторите попытку позже.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}