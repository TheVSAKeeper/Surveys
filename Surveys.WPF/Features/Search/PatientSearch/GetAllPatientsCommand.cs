using System.Collections.ObjectModel;
using Calabonga.OperationResults;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Search.PatientSearch;

public class GetAllPatientsCommand(PatientSearchFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<List<Patient>> result = await mediator.Send(new GetAllPatientsRequest());

        if (result.Ok)
            viewModel.Patients = new ObservableCollection<Patient>(result.Result!);
    }
}