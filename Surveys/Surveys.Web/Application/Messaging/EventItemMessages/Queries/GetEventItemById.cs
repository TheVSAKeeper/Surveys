using Surveys.Web.Application.Messaging.EventItemMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
///     EventItem by Identifier
/// </summary>
public sealed class GetEventItemById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        /// <summary>Handles a request getting log by identifier</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<Operation<EventItemViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<EventItem> repository = unitOfWork.GetRepository<EventItem>();
            EventItem? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: eventItem => eventItem.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            EventItemViewModel? mapped = mapper.Map<EventItemViewModel>(entityWithoutIncludes);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<EventItemViewModel, string>>;
}