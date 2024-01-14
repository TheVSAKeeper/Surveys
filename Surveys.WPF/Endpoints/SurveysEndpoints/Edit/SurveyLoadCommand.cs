using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyLoadCommand(SurveyEditFormViewModel viewModel, IMediator mediator, Guid surveyId)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<SurveyEditDto> result = await mediator.Send(new SurveyGetForEditRequest(surveyId));

        viewModel.Survey = result.Result;
    }

    protected override bool CanExecuteAsync(object? parameter) => viewModel is
    {
        Survey: null
    };
}