using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.Queries;

public sealed class GetAnamnesisTemplateById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnamnesisTemplateViewModel, string>>
    {
        public async Task<Operation<AnamnesisTemplateViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<AnamnesisTemplate> repository = unitOfWork.GetRepository<AnamnesisTemplate>();
            AnamnesisTemplate? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: anamnesisTemplate => anamnesisTemplate.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            AnamnesisTemplateViewModel? mapped = mapper.Map<AnamnesisTemplateViewModel>(entityWithoutIncludes);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            if (mapped.Questions == null)
                return Operation.Result(mapped);

            foreach (Question question in mapped.Questions)
                question.AnamnesisTemplate = null;

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<AnamnesisTemplateViewModel, string>>;
}