using System.Windows.Markup;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Surveys.WPF.Endpoints.DiagnosisEndpoints;
using Surveys.WPF.Endpoints.DiagnosisEndpoints.ViewModels;
using Surveys.WPF.ViewModels.Base;

namespace Surveys.WPF.ViewModels;

[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
public class MainWindowViewModel : TitledViewModel
{
    private readonly IMediator? _mediator;

    private List<DiagnosisViewModel>? _diagnosis;

    public MainWindowViewModel() : base("MainWindow")
    {
        _mediator = App.Services.GetService<IMediator>();
        
        ShowDiagnosis();
    }

    public List<DiagnosisViewModel>? Diagnosis
    {
        get => _diagnosis;
        set => Set(ref _diagnosis, value);
    }

    private async void ShowDiagnosis()
    {
        OperationResult<List<DiagnosisViewModel>> result = await _mediator.Send(new DiagnosisGetAllRequest());
        Diagnosis = result.Result;
    }
}