using System.Windows.Input;
using MediatR;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyEditFormViewModel : ViewModelBase
{
    private SurveyEditDto? _survey;

    public SurveyEditFormViewModel(
        IMediator mediator,
        Guid surveyId
    )
    {
        LoadCommand = new SurveyLoadCommand(this, mediator, surveyId);
        SubmitCommand = new SurveyUpdateCommand(this, mediator);
    }

    public SurveyEditDto? Survey
    {
        get => _survey;
        set => Set(ref _survey, value);
    }

    public ICommand SubmitCommand { get; }
    public ICommand LoadCommand { get; }
}