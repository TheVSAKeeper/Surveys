using Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Survey;

public class SurveyViewModel(
    SurveyShowAllFormViewModel showAllFormViewModel)
    : ViewModelBase
{
    public SurveyShowAllFormViewModel SurveyShowAllFormViewModel { get; } = showAllFormViewModel;
}