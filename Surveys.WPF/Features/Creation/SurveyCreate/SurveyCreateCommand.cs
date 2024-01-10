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
        OperationResult<Survey> result = await mediator.Send(new SurveyCreateRequest(viewModel.Patient!,
            viewModel.AnamnesesCreateFormViewModel.CreatedAnamneses!));

        viewModel.CreatedSurvey = result.Result;
    }

    protected override bool CanExecuteAsync(object? parameter) => viewModel is
    {
        Patient: not null,
        AnamnesesCreateFormViewModel.CreatedAnamneses: not null
    };
}