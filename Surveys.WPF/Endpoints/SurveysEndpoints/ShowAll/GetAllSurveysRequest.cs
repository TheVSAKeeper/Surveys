﻿using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

public record GetAllSurveysRequest : IRequest<OperationResult<List<SurveyShowDto>>>;

public class GetAllSurveysRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSurveysRequest, OperationResult<List<SurveyShowDto>>>
{
    public async Task<OperationResult<List<SurveyShowDto>>> Handle(GetAllSurveysRequest request, CancellationToken cancellationToken)
    {
        IList<SurveyShowDto> surveys = await unitOfWork.GetRepository<Survey>()
            .GetAllAsync(survey => new SurveyShowDto
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
                include: i => i.Include(survey => survey.Patient)!,
                disableTracking: true);

        return OperationResult.CreateResult(surveys.ToList());
    }
}