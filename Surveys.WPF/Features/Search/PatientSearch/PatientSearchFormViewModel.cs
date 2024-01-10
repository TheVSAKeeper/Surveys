﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation.Modal;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Search.PatientSearch;

public class PatientSearchFormViewModel : ViewModelBase, ICallbackViewModel<Patient>
{
    private ObservableCollection<Patient>? _patients;
    private Patient? _selectedPatient;

    /*public PatientSearchFormViewModel()
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
    }*/

    public PatientSearchFormViewModel(IMediator mediator, IMapper mapper, CloseModalNavigationService closeNavigationService)
    {
        RefreshCommand = new GetAllPatientsCommand(this, mediator);
        CancelCommand = new NavigateCommand(closeNavigationService);
    }

    public ICommand RefreshCommand { get; }
    public ICommand CancelCommand { get; }

    public ObservableCollection<Patient>? Patients
    {
        get => _patients;
        set => Set(ref _patients, value);
    }

    public Patient? SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            if (Set(ref _selectedPatient, value) == false)
                return;

            if (_selectedPatient != null)
                Callback?.Invoke(_selectedPatient);
        }
    }

    public event Action<Patient>? Callback;
}