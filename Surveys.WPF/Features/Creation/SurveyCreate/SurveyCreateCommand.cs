using Calabonga.OperationResults;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public class SurveyCreateCommand(SurveyCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        if (CanExecute(parameter))
        {
            OperationResult<Survey> result = await mediator.Send(new SurveyCreateRequest());
            viewModel.CreatedSurvey = result.Result;
        }
    }

    public override bool CanExecute(object? parameter) => IsExecuting == false && viewModel.Patient != null;
}