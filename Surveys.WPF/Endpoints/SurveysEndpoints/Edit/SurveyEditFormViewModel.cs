using System.Windows.Input;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyEditFormViewModel : ViewModelBase, IParameterViewModel<Guid>
{
    private Guid _loadedId;
    private List<Anamnesis>? _anamneses;
    private SurveyEditDto? _survey;

    public SurveyEditFormViewModel(IMediator mediator)
    {
        LoadCommand = new SurveyLoadCommand(this, mediator);
        SubmitCommand = new SurveyUpdateCommand(this, mediator);
    }

    public SurveyEditDto? Survey
    {
        get => _survey;
        set => Set(ref _survey, value);
    }

    public Guid LoadedId
    {
        get => _loadedId;
        private set => Set(ref _loadedId, value);
    }

    public List<Anamnesis>? Anamneses
    {
        get => _anamneses;
        set => Set(ref _anamneses, value);
    }

    public ICommand SubmitCommand { get; }
    public ICommand LoadCommand { get; }

    public void SetParameter(Guid parameter)
    {
        LoadedId = parameter;
        LoadCommand.Execute(parameter);
    }
}