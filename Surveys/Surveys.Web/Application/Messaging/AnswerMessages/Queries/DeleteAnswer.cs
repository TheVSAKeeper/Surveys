using Surveys.Web.Application.Messaging.AnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnswerMessages.Queries;

public sealed class DeleteAnswer
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnswerViewModel, string>>
    {
        public async Task<Operation<AnswerViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<Answer> repository = unitOfWork.GetRepository<Answer>();
            Answer? entity = await repository.FindAsync([request.Id], cancellationToken);

            if (entity == null)
                return Operation.Error("Entity not found");

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            if (unitOfWork.LastSaveChangesResult.IsOk == false)
                return Operation.Error(unitOfWork.LastSaveChangesResult.Exception?.Message ?? AppData.Exceptions.SomethingWrong);

            AnswerViewModel? mapped = mapper.Map<AnswerViewModel>(entity);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<AnswerViewModel, string>>;
}