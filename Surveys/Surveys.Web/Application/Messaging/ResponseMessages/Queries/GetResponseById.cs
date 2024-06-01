using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.Queries;

public sealed class GetResponseById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<ResponseViewModel, string>>
    {
        public async Task<Operation<ResponseViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Response> repository = unitOfWork.GetRepository<Response>();
            Response? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: response => response.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            ResponseViewModel? mapped = mapper.Map<ResponseViewModel>(entityWithoutIncludes);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<ResponseViewModel, string>>;
}