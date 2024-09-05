using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.Queries;

public sealed class DeleteResponse
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<ResponseViewModel, string>>
    {
        public async Task<Operation<ResponseViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<Response> repository = unitOfWork.GetRepository<Response>();
            Response? entity = await repository.FindAsync([request.Id], cancellationToken);

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

            ResponseViewModel? mapped = mapper.Map<ResponseViewModel>(entity);

            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<ResponseViewModel, string>>;
}
