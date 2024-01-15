using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Exceptions;
using Surveys.WPF.Endpoints.AuthenticationEndpoints;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Create;

public record SurveyCreateRequest(Patient Patient, List<Anamnesis> CreatedAnamneses) : IRequest<OperationResult<Survey>>;

public class SurveyCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<SurveyCreateRequest, OperationResult<Survey>>
{
    public async Task<OperationResult<Survey>> Handle(SurveyCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<Survey> result = OperationResult.CreateResult<Survey>();

        Survey survey = new()
        {
            Complaint = "Не указана",
            CreatedBy = authenticationStore.Username,
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

        List<Guid> anamnesesId = request.CreatedAnamneses.Select(x => x.Id).ToList();

        IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();

        IList<Anamnesis> entities = await repository.GetAllAsync(predicate: p => anamnesesId.Contains(p.Id),
            disableTracking: false);

        if (entities is [])
        {
            result.AddError(new SurveysNotFoundException(nameof(Anamnesis), Guid.Empty.ToString()));
            return result;
        }

        foreach (Anamnesis anamnesis in entities)
        {
            anamnesis.SurveyId = survey.Id;
            repository.Update(anamnesis);
        }

        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            result.AddError(unitOfWork.LastSaveChangesResult.Exception
                            ?? new SurveysDatabaseSaveException(nameof(Anamnesis)));

            return result;
        }

        return result;
    }
}