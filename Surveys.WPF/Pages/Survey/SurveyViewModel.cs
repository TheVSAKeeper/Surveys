using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;
using Surveys.WPF.Shared.Navigation.Modal;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Survey;

public class SurveyViewModel(
    ICallbackNavigationService<Patient> patientsModalNavigationService,
    ICallbackNavigationService<List<Anamnesis>> anamnesesModalNavigationService,
    IMediator mediator,
    IMapper mapper)
    : ViewModelBase
{
    public SurveyShowAllFormViewModel SurveyShowAllFormViewModel { get; } = new(mediator);
}