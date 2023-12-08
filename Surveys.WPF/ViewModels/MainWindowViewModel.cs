using System.Windows.Input;
using System.Windows.Markup;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Surveys.WPF.Application.Commands;
using Surveys.WPF.Endpoints.DiagnosisEndpoints;
using Surveys.WPF.Endpoints.DiagnosisEndpoints.ViewModels;
using Surveys.WPF.ViewModels.Base;

namespace Surveys.WPF.ViewModels;

[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
public class MainWindowViewModel() : TitledViewModel("Главная страница")
{
    private readonly IMediator _mediator = App.Services.GetRequiredService<IMediator>();

    private List<DiagnosisViewModel>? _diagnosis;

    public List<DiagnosisViewModel>? Diagnosis
    {
        get => _diagnosis;
        set => Set(ref _diagnosis, value);
    }

    #region ShowAllDiagnosis

    private ICommand? _showAllDiagnosisCommand;

    public ICommand ShowAllDiagnosisCommand => _showAllDiagnosisCommand
        ??= new LambdaCommand(OnShowAllDiagnosisCommandExecuted, _ => _diagnosis is null);

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
        ??= new LambdaCommand(OnClearTableCommandExecuted, _ => _diagnosis is not null);

    private void OnClearTableCommandExecuted(object? parameter)
    {
        Diagnosis = null;
    }

    #endregion
}