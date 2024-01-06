using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Search.PatientSearch;

public class PatientSearchFormViewModel : ViewModelBase
{
    private ObservableCollection<Patient>? _patients;

    public PatientSearchFormViewModel()
    {
        Patients = new ObservableCollection<Patient>(Enumerable.Range(0, 20)
            .Select(x => new Patient
            {
                LastName = $"LastName {x}",
                FirstName = $"FirstName {x}",
                Patronymic = $"Patronymic {x}",
                Gender = Gender.Male,
                BirthDate = new DateOnly()
            })
            .ToList());
    }

    public PatientSearchFormViewModel(IMediator mediator, IMapper mapper)
    {
        RefreshCommand = new GetAllPatientsCommand(this, mediator);
    }

    public ICommand RefreshCommand { get; }

    public ObservableCollection<Patient>? Patients
    {
        get => _patients;
        set => Set(ref _patients, value);
    }

    // public event Action<List<Anamnesis>>? AnamnesesCreated;
}