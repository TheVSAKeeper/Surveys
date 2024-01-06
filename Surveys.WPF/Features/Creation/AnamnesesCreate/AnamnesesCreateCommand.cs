using System.Windows;
using Calabonga.OperationResults;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Creation.AnamnesesCreate;

public class AnamnesesCreateCommand(AnamnesesCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<List<Anamnesis>> result =
            await mediator.Send(new AnamnesesCreateRequest(viewModel.AnamnesisTemplates!.ToList()));

        if (result.Ok)
        {
            MessageBox.Show($"Создано {result.Result!.Count} анамнезов", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
            viewModel.CreatedAnamneses = result.Result;
        }
    }

    public override bool CanExecute(object? parameter) => IsExecuting == false
                                                          && viewModel.AnamnesisTemplates != null
                                                          && viewModel.AnamnesisTemplates.Any(template => template.IsSelected);
}