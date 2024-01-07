using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Features.Creation.AnamnesesCreate;
using Surveys.WPF.Features.Search.PatientSearch;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public class SurveyCreateFormViewModel : ViewModelBase
{
    private ObservableCollection<Anamnesis>? _anamneses;
    private Patient? _patient;
    private Survey? _createdSurvey;

    public SurveyCreateFormViewModel(IMediator mediator, IMapper mapper)
    {
        SubmitCommand = new SurveyCreateCommand(this, mediator);
        
        AnamnesesCreateFormViewModel = new AnamnesesCreateFormViewModel(mediator, mapper);
        PatientSearchFormViewModel = new PatientSearchFormViewModel(mediator, mapper);

        AnamnesesCreateFormViewModel.AnamnesesCreated += OnAnamnesesCreated;
        PatientSearchFormViewModel.PatientSelected += OnPatientSelected;
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

    public AnamnesesCreateFormViewModel AnamnesesCreateFormViewModel { get; }
    public PatientSearchFormViewModel PatientSearchFormViewModel { get; }

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

    public override void Dispose()
    {
        base.Dispose();

        AnamnesesCreateFormViewModel.AnamnesesCreated -= OnAnamnesesCreated;
        PatientSearchFormViewModel.PatientSelected -= OnPatientSelected;
    }
}