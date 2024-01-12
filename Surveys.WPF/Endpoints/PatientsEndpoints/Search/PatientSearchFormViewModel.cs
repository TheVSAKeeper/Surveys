using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation.Modal;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.PatientsEndpoints.Search;

public class PatientSearchFormViewModel : ViewModelBase, ICallbackViewModel<Patient>
{
    private Action<Patient>? _callback;
    private ICommand? _confirmCommand;
    private ObservableCollection<Patient>? _patients;
    private Patient? _confirmedPatient;
    private Patient? _selectedPatient;

    public PatientSearchFormViewModel(IMediator mediator, IMapper mapper, CloseModalNavigationService closeNavigationService)
    {
        RefreshCommand = new GetAllPatientsCommand(this, mediator);
        CancelCommand = new NavigateCommand(closeNavigationService);
    }

    public ICommand RefreshCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand ConfirmCommand => _confirmCommand ??= new LambdaCommand(() =>
        {
            ConfirmedPatient = SelectedPatient;
            CancelCommand.Execute(null);
        },
        () => ConfirmedPatient != SelectedPatient);

    public ObservableCollection<Patient>? Patients
    {
        get => _patients;
        set => Set(ref _patients, value);
    }

    public Patient? SelectedPatient
    {
        get => _selectedPatient;
        set => Set(ref _selectedPatient, value);
    }

    private Patient? ConfirmedPatient
    {
        get => _confirmedPatient;
        set
        {
            if (Set(ref _confirmedPatient, value) == false)
                return;

            if (_confirmedPatient != null)
                _callback?.Invoke(_confirmedPatient);
        }
    }

    public void SetCallback(Action<Patient> callback) => _callback ??= callback;
}