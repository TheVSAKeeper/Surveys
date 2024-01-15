using System.Collections.ObjectModel;
using System.Windows.Input;
using MediatR;
using Surveys.WPF.Endpoints.SurveysEndpoints.Create;
using Surveys.WPF.Endpoints.SurveysEndpoints.Edit;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

public class SurveyShowAllFormViewModel : ViewModelBase
{
    private ICommand? _editSurveyCommand;
    private ObservableCollection<SurveyShowDto>? _surveys;
    private SurveyShowDto? _selectedSurvey;

    public SurveyShowAllFormViewModel(
        IMediator mediator,
        NavigationService<SurveyCreateFormViewModel> surveyCreateNavigationService,
        ParameterNavigationService<Guid, SurveyEditFormViewModel> surveyEditNavigationService)
    {
        RefreshCommand = new GetAllSurveysCommand(this, mediator);
        CreateSurveyCommand = new NavigateCommand(surveyCreateNavigationService);
        EditSurveyNavigateCommand = new ParameterNavigateCommand<Guid>(surveyEditNavigationService);
    }

    public ICommand RefreshCommand { get; }
    public ICommand CreateSurveyCommand { get; }
    public ICommand EditSurveyNavigateCommand { get; }

    public ObservableCollection<SurveyShowDto>? Surveys
    {
        get => _surveys;
        set => Set(ref _surveys, value);
    }

    public SurveyShowDto? SelectedSurvey
    {
        get => _selectedSurvey;
        set => Set(ref _selectedSurvey, value);
    }

    public ICommand EditSurveyCommand => _editSurveyCommand ??= new LambdaCommand(id =>
    {
        EditSurveyNavigateCommand.Execute(id);
    });
}