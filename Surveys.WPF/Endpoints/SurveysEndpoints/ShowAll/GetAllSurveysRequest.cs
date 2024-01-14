using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

public record GetAllSurveysRequest : IRequest<OperationResult<List<SurveyDto>>>;

public class GetAllSurveysRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSurveysRequest, OperationResult<List<SurveyDto>>>
{
    public async Task<OperationResult<List<SurveyDto>>> Handle(GetAllSurveysRequest request, CancellationToken cancellationToken)
    {
        IList<SurveyDto> surveys = await unitOfWork.GetRepository<Survey>()
            .GetAllAsync(survey => new SurveyDto
                {
                    Complaint = survey.Complaint,
                    Patient = survey.Patient,
                    CreatedAt = survey.CreatedAt,
                    CreatedBy = survey.CreatedBy,
                    IsComplete = survey.IsComplete,
                    UpdatedAt = survey.UpdatedAt,
                    UpdatedBy = survey.UpdatedBy,
                    Id = survey.Id
                },
                survey => survey.IsComplete == false,
                include: i => i.Include(survey => survey.Patient),
                disableTracking: true);

        return OperationResult.CreateResult(surveys.ToList());
    }
}