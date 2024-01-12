using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;
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
        IMapper mapper,
        ICallbackNavigationService<Patient> patientsModalNavigationService,
        ICallbackNavigationService<List<Anamnesis>> anamnesesModalNavigationService
    )
    {
        SubmitCommand = new SurveyCreateCommand(this, mediator);
        SearchPatientCommand = new CallbackNavigateCommand<Patient>(patientsModalNavigationService, OnPatientSelected);
        AddAnamnesesCommand = new CallbackNavigateCommand<List<Anamnesis>>(anamnesesModalNavigationService, OnAnamnesesCreated);
    }

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