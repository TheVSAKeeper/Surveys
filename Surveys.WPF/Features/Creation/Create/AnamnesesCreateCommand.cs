using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Creation.Create;

public class AnamnesesCreateCommand(AnamnesesCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        if (viewModel.AnamnesisTemplates != null)
        {
            OperationResult<List<Domain.Anamnesis>> result = await mediator.Send(new AnamnesesCreateRequest(viewModel.AnamnesisTemplates.ToList()));
            viewModel.CreatedAnamneses = result.Result;
        }
    }

    public override bool CanExecute(object? parameter) => IsExecuting == false
                                                          && viewModel.AnamnesisTemplates != null
                                                          && viewModel.AnamnesisTemplates.Any(dto => dto.IsSelected);
}