using Calabonga.OperationResults;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyLoadLastCommand(SurveyEditFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<List<Anamnesis>> result = await mediator.Send(new SurveyGetLastRequest(viewModel.Survey!));

        if (result.Ok == false)
            return;

        foreach (Anamnesis anamnesis in viewModel.Anamneses!)
        {
            foreach (Anamnesis oldAnamnesis in result.Result!)
            {
                if (anamnesis.AnamnesisTemplateId == oldAnamnesis.AnamnesisTemplateId && oldAnamnesis.AnamnesisAnswers!.Count !=0)
                    anamnesis.AnamnesisAnswers = new List<AnamnesisAnswer>(oldAnamnesis.AnamnesisAnswers!);
            }
        }

        viewModel.Anamneses = [..viewModel.Anamneses];
    }

    protected override bool CanExecuteAsync(object? parameter) => viewModel is
    {
        Survey.Patient: not null,
        Anamneses: not null
    };
}