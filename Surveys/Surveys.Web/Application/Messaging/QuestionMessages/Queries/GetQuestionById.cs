using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionMessages.Queries;

public sealed class GetQuestionById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<QuestionViewModel, string>>
    {
        public async Task<Operation<QuestionViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Question> repository = unitOfWork.GetRepository<Question>();
            Question? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: question => question.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            QuestionViewModel? mapped = mapper.Map<QuestionViewModel>(entityWithoutIncludes);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<QuestionViewModel, string>>;
}