using System.Collections.ObjectModel;
using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

public class GetAllSurveysCommand(SurveyShowAllFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<List<SurveyShowDto>> result = await mediator.Send(new GetAllSurveysRequest());

        if (result.Ok)
            viewModel.Surveys = new ObservableCollection<SurveyShowDto>(result.Result!);
    }
}