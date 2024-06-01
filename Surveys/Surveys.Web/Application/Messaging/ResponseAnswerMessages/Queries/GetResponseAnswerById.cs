using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.Queries;

public sealed class GetResponseAnswerById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<ResponseAnswerViewModel, string>>
    {
        public async Task<Operation<ResponseAnswerViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<ResponseAnswer> repository = unitOfWork.GetRepository<ResponseAnswer>();
            ResponseAnswer? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: responseAnswer => responseAnswer.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            ResponseAnswerViewModel? mapped = mapper.Map<ResponseAnswerViewModel>(entityWithoutIncludes);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<ResponseAnswerViewModel, string>>;
}