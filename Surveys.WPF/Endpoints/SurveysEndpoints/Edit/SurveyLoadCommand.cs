using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.Domain.Exceptions;
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

public class SurveyLoadLastCommand(SurveyEditFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<List<Anamnesis>> result = await mediator.Send(new SurveyGetLastRequest(viewModel.Survey!.Patient.Id));

        if (result.Ok == false)
            return;

        foreach (Anamnesis anamnesis in viewModel.Anamneses)
        {
            
        }
        viewModel.Anamneses = result.Result;
    }

    protected override bool CanExecuteAsync(object? parameter) => viewModel is
    {
        Survey: null
    };
}

public record SurveyGetLastRequest(Guid Id) : IRequest<OperationResult<List<Anamnesis>>>;

public class SurveyGetLastRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<SurveyGetLastRequest, OperationResult<List<Anamnesis>>>
{
    public async Task<OperationResult<List<Anamnesis>>> Handle(SurveyGetLastRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<Anamnesis>> result = OperationResult.CreateResult<List<Anamnesis>>();

        Survey? survey = await unitOfWork.GetRepository<Survey>()
            .GetFirstOrDefaultAsync(predicate: p => p.PatientId == request.Id,
                o => o.OrderByDescending(survey => survey.CreatedAt),
                include: i=> i
                    .Include(survey1 => survey1.Anamneses)
                    .AsSplitQuery()
                    .Include(survey1 => survey1.Patient));

        
        if (survey is null)
        {
            result.AddError(new SurveysNotFoundException(nameof(Patient), request.Id.ToString()));
            return result;
        }

        result.Result = survey.Anamneses!.ToList();

        return result;
    }
}