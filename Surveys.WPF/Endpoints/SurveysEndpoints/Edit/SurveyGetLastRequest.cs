using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.Domain.Exceptions;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public record SurveyGetLastRequest(SurveyEditDto Dto) : IRequest<OperationResult<List<Anamnesis>>>;

public class SurveyGetLastRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<SurveyGetLastRequest, OperationResult<List<Anamnesis>>>
{
    public async Task<OperationResult<List<Anamnesis>>> Handle(SurveyGetLastRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<Anamnesis>> result = OperationResult.CreateResult<List<Anamnesis>>();

        Survey? latestSurvey = await unitOfWork.GetRepository<Survey>()
            .GetFirstOrDefaultAsync(p => p.PatientId == request.Dto.Patient!.Id && p.Id != request.Dto.Id,
                o => o.OrderByDescending(survey => survey.CreatedAt),
                i => i
                    .Include(survey => survey.Anamneses)
                    .AsSplitQuery()
                    .Include(survey => survey.Patient)!);

        if (latestSurvey is null)
        {
            result.AddError(new SurveysNotFoundException(nameof(Patient), request.Dto.Patient!.Id.ToString()));
            return result;
        }

        result.Result = latestSurvey.Anamneses!.ToList();

        return result;
    }
}