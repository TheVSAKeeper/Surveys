using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.Queries;

public sealed class DeleteResponseAnswer
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<ResponseAnswerViewModel, string>>
    {
        public async Task<Operation<ResponseAnswerViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<ResponseAnswer> repository = unitOfWork.GetRepository<ResponseAnswer>();
            ResponseAnswer? entity = await repository.FindAsync([request.Id], cancellationToken);

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

            ResponseAnswerViewModel? mapped = mapper.Map<ResponseAnswerViewModel>(entity);

            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<ResponseAnswerViewModel, string>>;
}
