using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.Domain.Exceptions;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public record SurveyGetForEditRequest(Guid Id) : IRequest<OperationResult<SurveyEditDto>>;

public class SurveyGetForEditRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<SurveyGetForEditRequest, OperationResult<SurveyEditDto>>
{
    public async Task<OperationResult<SurveyEditDto>> Handle(SurveyGetForEditRequest request, CancellationToken cancellationToken)
    {
        OperationResult<SurveyEditDto> result = OperationResult.CreateResult<SurveyEditDto>();

        SurveyEditDto? surveyDto = await unitOfWork.GetRepository<Survey>()
            .GetFirstOrDefaultAsync(x => new SurveyEditDto
                {
                    Id = x.Id,
                    Complaint = x.Complaint,
                    Patient = x.Patient,
                    IsComplete = x.IsComplete,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    Anamneses = x.Anamneses
                },
                p => p.Id == request.Id,
                include: i => i.Include(survey => survey.Anamneses)
                    .AsSplitQuery()
                    .Include(survey => survey.Patient));

        if (surveyDto is null)
        {
            result.AddError(new SurveysNotFoundException(nameof(Survey), request.Id.ToString()));
            return result;
        }

        result.Result = surveyDto;

        return result;
    }
}