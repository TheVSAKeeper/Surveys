using System.Collections.ObjectModel;
using System.Windows.Input;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Endpoints.SurveysEndpoints.Edit;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.Navigation.Modal;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Create;

public class SurveyCreateFormViewModel : ViewModelBase
{
    private ObservableCollection<Anamnesis>? _anamneses;
    private Patient? _patient;
    private Survey? _createdSurvey;

    public SurveyCreateFormViewModel(
        IMediator mediator,
        ICallbackNavigationService<Patient> patientsModalNavigationService,
        ICallbackNavigationService<List<Anamnesis>> anamnesesModalNavigationService,
        ParameterNavigationService<Guid, SurveyEditFormViewModel> surveyEditNavigationService
    )
    {
        SubmitCommand = new SurveyCreateCommand(this, mediator);
        SearchPatientCommand = new CallbackNavigateCommand<Patient>(patientsModalNavigationService, OnPatientSelected);
        AddAnamnesesCommand = new CallbackNavigateCommand<List<Anamnesis>>(anamnesesModalNavigationService, OnAnamnesesCreated);
        EditSurveyNavigateCommand = new ParameterNavigateCommand<Guid>(surveyEditNavigationService);
    }

    public ICommand EditSurveyNavigateCommand { get; }

    public Patient? Patient
    {
        get => _patient;
        set => Set(ref _patient, value);
    }

    public Survey? CreatedSurvey
    {
        get => _createdSurvey;
        set => Set(ref _createdSurvey, value);
    }

    public ObservableCollection<Anamnesis>? Anamneses
    {
        get => _anamneses;
        set => Set(ref _anamneses, value);
    }

    public ICommand SubmitCommand { get; }
    public ICommand SearchPatientCommand { get; }
    public ICommand AddAnamnesesCommand { get; }

    private void OnPatientSelected(Patient patient)
    {
        Patient = patient;
    }

    private void OnAnamnesesCreated(List<Anamnesis> list)
    {
        List<Anamnesis> old;

        if (Anamneses == null)
        {
            old = list;
        }
        else
        {
            old = Anamneses.ToList();
            old.AddRange(list);
        }

        Anamneses = new ObservableCollection<Anamnesis>(old);
    }
}