using Surveys.Web.Application.Messaging.EventItemMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
///     EventItem delete
/// </summary>
public sealed class DeleteEventItem
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<Operation<EventItemViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<EventItem> repository = unitOfWork.GetRepository<EventItem>();
            EventItem? entity = await repository.FindAsync([request.Id], cancellationToken);

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

            EventItemViewModel? mapped = mapper.Map<EventItemViewModel>(entity);

            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<EventItemViewModel, string>>;
}
