using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public class SurveyCreateCommand(SurveyCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
    }
}