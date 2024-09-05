using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionOptionMessages.Queries;

public sealed class DeleteQuestionOption
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<QuestionOptionViewModel, string>>
    {
        public async Task<Operation<QuestionOptionViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<QuestionOption> repository = unitOfWork.GetRepository<QuestionOption>();
            QuestionOption? entity = await repository.FindAsync([request.Id], cancellationToken);

            if (entity == null)
            {
                return Operation.Error("Entity not found");
            }

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            if (unitOfWork.LastSaveChangesResult.IsOk == false)
            {
                return Operation.Error(unitOfWork.LastSaveChangesResult.Exception?.Message ?? AppData.Exceptions.SomethingWrong);
            }

            QuestionOptionViewModel? mapped = mapper.Map<QuestionOptionViewModel>(entity);

            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<QuestionOptionViewModel, string>>;
}
