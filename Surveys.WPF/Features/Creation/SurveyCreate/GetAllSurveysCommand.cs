using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public class GetAllSurveysCommand(SurveyCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<List<SurveyDto>> result = await mediator.Send(new GetAllSurveysRequest());

        if (result.Ok)
            ;
        //viewModel.Surveys = new ObservableCollection<SurveyDto>(result.Result!);
    }
}