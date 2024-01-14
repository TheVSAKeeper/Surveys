using System.Collections.ObjectModel;
using System.Windows.Input;
using MediatR;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

public class SurveyShowAllFormViewModel : ViewModelBase
{
    private ObservableCollection<SurveyDto>? _surveys;
    private SurveyDto? _selectedSurvey;

    public SurveyShowAllFormViewModel(IMediator mediator)
    {
        RefreshCommand = new GetAllSurveysCommand(this, mediator);
    }

    public ICommand RefreshCommand { get; }

    public ObservableCollection<SurveyDto>? Surveys
    {
        get => _surveys;
        set => Set(ref _surveys, value);
    }

    public SurveyDto? SelectedSurvey
    {
        get => _selectedSurvey;
        set => Set(ref _selectedSurvey, value);
    }
}