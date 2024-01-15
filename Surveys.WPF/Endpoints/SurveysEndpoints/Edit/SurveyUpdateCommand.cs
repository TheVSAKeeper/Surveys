using System.Windows;
using Calabonga.OperationResults;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyUpdateCommand(SurveyEditFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        foreach (AnamnesisAnswer anamnesisAnswer in viewModel.Anamneses!.SelectMany(anamnesis => anamnesis.AnamnesisAnswers!))
            anamnesisAnswer.Answers = anamnesisAnswer.Answers!.Where(answer => string.IsNullOrWhiteSpace(answer.Content) == false).ToList();

        viewModel.Survey!.Anamneses = viewModel.Anamneses;

        OperationResult<Guid> result = await mediator.Send(new SurveyUpdateRequest(viewModel.Survey!));

        if (result.Ok == false)
            MessageBox.Show("Ошибка обновления опроса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    protected override bool CanExecuteAsync(object? parameter) => viewModel is
    {
        Survey: not null
    };
}