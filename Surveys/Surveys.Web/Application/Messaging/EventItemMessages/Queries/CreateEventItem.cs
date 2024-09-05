using Calabonga.Microservices.Core;
using Surveys.Web.Application.Messaging.EventItemMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
///     Request: EventItem creation
/// </summary>
public sealed class CreateEventItem
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        public async Task<Operation<EventItemViewModel, string>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new EventItem");

            EventItem? entity = mapper.Map<EventItemCreateViewModel, EventItem>(eventItemRequest.Model);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppContracts.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<EventItem>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                EventItemViewModel? mapped = mapper.Map<EventItem, EventItemViewModel>(entity);

                if (mapped is null)
                {
                    return Operation.Error(AppData.Exceptions.MappingException);
                }

                logger.LogInformation("New entity {@EventItem} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(EventItemCreateViewModel Model) : IRequest<Operation<EventItemViewModel, string>>;
}
