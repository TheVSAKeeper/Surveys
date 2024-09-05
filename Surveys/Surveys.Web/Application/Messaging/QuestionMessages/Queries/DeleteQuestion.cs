using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionMessages.Queries;

public sealed class DeleteQuestion
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<QuestionViewModel, string>>
    {
        public async Task<Operation<QuestionViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<Question> repository = unitOfWork.GetRepository<Question>();
            Question? entity = await repository.FindAsync([request.Id], cancellationToken);

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

            QuestionViewModel? mapped = mapper.Map<QuestionViewModel>(entity);

            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<QuestionViewModel, string>>;
}
