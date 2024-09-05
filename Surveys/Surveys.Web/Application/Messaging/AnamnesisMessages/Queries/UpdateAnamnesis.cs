using System.Security.Claims;
using Surveys.Infrastructure;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

public sealed class UpdateAnamnesis
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnamnesisViewModel, string>>
    {
        public async Task<Operation<AnamnesisViewModel, string>> Handle(Request anamnesisRequest, CancellationToken cancellationToken)
        {
            IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();

            Anamnesis? entity = await repository.GetFirstOrDefaultAsync(predicate: anamnesis => anamnesis.Id == anamnesisRequest.Id, disableTracking: false);

            if (entity == null)
            {
                return Operation.Error(AppData.Exceptions.NotFoundException);
            }

            mapper.Map(anamnesisRequest.Model, entity, options => options.Items[nameof(ApplicationUser)] = anamnesisRequest.User.Identity!.Name);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                AnamnesisViewModel? mapped = mapper.Map<Anamnesis, AnamnesisViewModel>(entity);

                if (mapped is not null)
                {
                    return Operation.Result(mapped);
                }

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, AnamnesisUpdateViewModel Model, ClaimsPrincipal User) : IRequest<Operation<AnamnesisViewModel, string>>;
}
