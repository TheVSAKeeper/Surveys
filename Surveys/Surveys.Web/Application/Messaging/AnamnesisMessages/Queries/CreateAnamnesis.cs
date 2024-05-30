using System.Security.Claims;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.AnamnesisMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

public sealed class CreateAnamnesis
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<AnamnesisViewModel, string>>
    {
        public async Task<Operation<AnamnesisViewModel, string>> Handle(Request anamnesisRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new Anamnesis");

            Anamnesis? entity = mapper.Map<AnamnesisCreateViewModel, Anamnesis>(anamnesisRequest.Model,
                options => options.Items[nameof(ApplicationUser)] = anamnesisRequest.User.Identity!.Name);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<Anamnesis>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                AnamnesisViewModel? mapped = mapper.Map<Anamnesis, AnamnesisViewModel>(entity);

                if (mapped is null)
                    return Operation.Error(AppData.Exceptions.MappingException);

                logger.LogInformation("New entity {@Anamnesis} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(AnamnesisCreateViewModel Model, ClaimsPrincipal User) : IRequest<Operation<AnamnesisViewModel, string>>;
}