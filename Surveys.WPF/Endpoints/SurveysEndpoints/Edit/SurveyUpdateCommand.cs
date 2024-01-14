using System.Windows;
using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyUpdateCommand(SurveyEditFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<Guid> result = await mediator.Send(new SurveyUpdateRequest(viewModel.Survey!));

        if (result.Ok == false)
            MessageBox.Show("Ошибка обновления опроса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    protected override bool CanExecuteAsync(object? parameter) => viewModel is
    {
        Survey: not null
    };
}