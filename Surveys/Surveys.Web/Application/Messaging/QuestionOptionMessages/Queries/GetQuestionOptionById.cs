using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionOptionMessages.Queries;

public sealed class GetQuestionOptionById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<QuestionOptionViewModel, string>>
    {
        public async Task<Operation<QuestionOptionViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<QuestionOption> repository = unitOfWork.GetRepository<QuestionOption>();
            QuestionOption? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: questionOption => questionOption.Id == id);

            if (entityWithoutIncludes == null)
            {
                return Operation.Error($"Entity with identifier {id} not found");
            }

            QuestionOptionViewModel? mapped = mapper.Map<QuestionOptionViewModel>(entityWithoutIncludes);

            if (mapped is null)
            {
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<QuestionOptionViewModel, string>>;
}
