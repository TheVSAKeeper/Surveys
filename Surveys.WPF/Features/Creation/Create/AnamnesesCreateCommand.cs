using System.Windows;
using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Creation.Create;

public class AnamnesesCreateCommand(AnamnesesCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<Domain.Anamnesis> result = await mediator.Send(new AnamnesesCreateRequest());

        if (result.Ok)
            MessageBox.Show("Пользователь обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
        else
            MessageBox.Show("Ошибка обновления пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public override bool CanExecute(object? parameter) => IsExecuting == false && viewModel.AnamnesisTemplate is not null;
}