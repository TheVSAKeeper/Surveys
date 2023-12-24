using System.Windows;
using System.Windows.Markup;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Surveys.WPF.Endpoints.DiagnosisEndpoints.ViewModels;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.ViewModels.Base;

namespace Surveys.WPF.ViewModels;

[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
public class MainWindowViewModel : TitledViewModel
{
    private readonly IMediator _mediator = App.Services.GetRequiredService<IMediator>();

    private List<DiagnosisViewModel>? _diagnosis;

    public MainWindowViewModel() : base("Главная страница")
    {
        NewMethod();
    }

    public List<DiagnosisViewModel>? Diagnosis
    {
        get => _diagnosis;
        set => Set(ref _diagnosis, value);
    }

    private static async void NewMethod()
    {
        AuthenticationStore authenticationStore = App.Services.GetRequiredService<AuthenticationStore>();

        if (authenticationStore.IsLoggedIn)
        {
            MessageBox.Show($"Loaded user {authenticationStore.User?.FirstName}");
            return;
        }

        SignInResult result = await authenticationStore.SignInAsync("Superuser", "123qwe");

        MessageBox.Show(result.Succeeded ? $"User logged in successfully {authenticationStore.User?.FirstName}" : "Invalid username or password");
    }

    /*#region ShowAllDiagnosis

    private ICommand? _showAllDiagnosisCommand;

    public ICommand ShowAllDiagnosisCommand => _showAllDiagnosisCommand
        ??= new LambdaCommand(OnShowAllDiagnosisCommandExecuted,
            _ => _diagnosis is null);

    private void OnShowAllDiagnosisCommandExecuted(object? parameter)
    {
        ShowDiagnosis();
    }

    private async void ShowDiagnosis()
    {
        OperationResult<List<DiagnosisViewModel>> result = await _mediator.Send(new DiagnosisGetAllRequest());
        Diagnosis = result.Result;
    }

    #endregion

    #region ClearTable

    private ICommand? _clearTable;

    public ICommand ClearTable => _clearTable
        ??= new LambdaCommand(OnClearTableCommandExecuted,
            _ => _diagnosis is not null);

    private void OnClearTableCommandExecuted(object? parameter)
    {
        Diagnosis = null;
    }

    #endregion*/
}