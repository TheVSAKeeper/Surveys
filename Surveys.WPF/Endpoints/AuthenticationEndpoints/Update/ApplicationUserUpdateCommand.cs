using System.Windows;
using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.AuthenticationEndpoints.Update;

public class ApplicationUserUpdateCommand(ApplicationUserUpdateViewModel viewModel, IMediator mediator, AuthenticationStore authenticationStore)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        viewModel.Validate();

        if (viewModel.HasErrors)
            return;

        OperationResult<Guid> result = await mediator.Send(new ApplicationUserUpdateRequest(viewModel.ApplicationUser));
        await authenticationStore.UpdateUserAsync(result.Result);

        if (result.Ok)
            MessageBox.Show("Пользователь обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
        else
            MessageBox.Show("Ошибка обновления пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}