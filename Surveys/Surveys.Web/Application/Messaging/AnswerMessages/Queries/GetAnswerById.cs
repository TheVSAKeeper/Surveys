using Surveys.Web.Application.Messaging.AnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnswerMessages.Queries;

public sealed class GetAnswerById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnswerViewModel, string>>
    {
        public async Task<Operation<AnswerViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Answer> repository = unitOfWork.GetRepository<Answer>();
            Answer? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: Answer => Answer.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            AnswerViewModel? mapped = mapper.Map<AnswerViewModel>(entityWithoutIncludes);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<AnswerViewModel, string>>;
}