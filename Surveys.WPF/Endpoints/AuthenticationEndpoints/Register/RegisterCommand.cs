using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.Identity;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.AuthenticationEndpoints.Register;

public class RegisterCommand(
    RegisterFormViewModel registerViewModel,
    AuthenticationManager authenticationManager)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            if (parameter is not PasswordBox passwordBox)
                return;

            IdentityResult result = await authenticationManager.CreateUserAsync(registerViewModel.Username!, passwordBox.Password, registerViewModel.Role!);

            if (result.Succeeded == false)
            {
                string errors = string.Join(Environment.NewLine, result.Errors.Select(error => error.Description));
                MessageBox.Show($"Ошибка регистрации. {Environment.NewLine}{errors}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Успешно зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception)
        {
            MessageBox.Show("Регистрация не удалась. Пожалуйста, проверьте вашу информацию или повторите попытку позже.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override bool CanExecuteAsync(object? parameter) => registerViewModel is
    {
        Username: not null,
        Role: not null
    };
}