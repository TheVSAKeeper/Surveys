using Calabonga.Microservices.Core;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.PatientMessages.Queries;

public sealed class CreatePatient
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<PatientViewModel, string>>
    {
        public async Task<Operation<PatientViewModel, string>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new Patient");

            Patient? entity = mapper.Map<PatientCreateViewModel, Patient>(eventItemRequest.Model);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppContracts.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<Patient>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                PatientViewModel? mapped = mapper.Map<Patient, PatientViewModel>(entity);

                if (mapped is null)
                    return Operation.Error(AppData.Exceptions.MappingException);

                logger.LogInformation("New entity {@Patient} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(PatientCreateViewModel Model) : IRequest<Operation<PatientViewModel, string>>;
}