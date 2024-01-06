using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Exceptions;
using Surveys.WPF.Features.Authentication;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public record SurveyCreateRequest(Patient Patient, List<Anamnesis> CreatedAnamneses) : IRequest<OperationResult<Survey>>;

public class SurveyCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<SurveyCreateRequest, OperationResult<Survey>>
{
    public async Task<OperationResult<Survey>> Handle(SurveyCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<Survey> result = OperationResult.CreateResult<Survey>();

        Survey survey = new()
        {
            CreatedBy = authenticationStore.Username,
            Anamneses = request.CreatedAnamneses.Select(x => new Anamnesis { Id = x.Id }).ToList(),
            PatientId = request.Patient.Id
        };

        await unitOfWork.GetRepository<Survey>().InsertAsync(survey, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            result.AddError(unitOfWork.LastSaveChangesResult.Exception
                            ?? new SurveysDatabaseSaveException(nameof(Survey)));

            return result;
        }

        result.Result = survey;

        return result;
    }
}