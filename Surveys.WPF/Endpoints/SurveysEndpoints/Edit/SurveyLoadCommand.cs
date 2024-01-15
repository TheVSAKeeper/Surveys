using Calabonga.OperationResults;
using MediatR;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyLoadCommand(SurveyEditFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<SurveyEditDto> result = await mediator.Send(new SurveyGetForEditRequest(viewModel.LoadedId));

        if (result.Ok == false)
            return;

        viewModel.Survey = result.Result;
        //  viewModel.Anamneses = new ObservableCollectionListSource<Anamnesis>(result.Result!.Anamneses!);
        viewModel.Anamneses = result.Result!.Anamneses!.ToList();
    }

    protected override bool CanExecuteAsync(object? parameter) => viewModel is
    {
        Survey: null
    };
}