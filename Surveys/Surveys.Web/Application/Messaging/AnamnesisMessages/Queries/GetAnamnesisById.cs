using Surveys.Web.Application.Messaging.AnamnesisMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

public sealed class GetAnamnesisById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnamnesisViewModel, string>>
    {
        public async Task<Operation<AnamnesisViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();
            Anamnesis? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: anamnesis => anamnesis.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            AnamnesisViewModel? mapped = mapper.Map<AnamnesisViewModel>(entityWithoutIncludes);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<AnamnesisViewModel, string>>;
}