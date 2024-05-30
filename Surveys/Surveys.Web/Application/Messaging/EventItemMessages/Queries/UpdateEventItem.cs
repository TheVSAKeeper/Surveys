using Calabonga.Microservices.Core;
using Surveys.Web.Application.Messaging.EventItemMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
///     Request: EventItem edit
/// </summary>
public sealed class UpdateEventItem
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        public async Task<Operation<EventItemViewModel, string>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            IRepository<EventItem> repository = unitOfWork.GetRepository<EventItem>();

            EventItem? entity = await repository.GetFirstOrDefaultAsync(predicate: eventItem => eventItem.Id == eventItemRequest.Id, disableTracking: false);

            if (entity == null)
                return Operation.Error(AppContracts.Exceptions.NotFoundException);

            mapper.Map(eventItemRequest.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                EventItemViewModel? mapped = mapper.Map<EventItem, EventItemViewModel>(entity);

                if (mapped is not null)
                    return Operation.Result(mapped);

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, EventItemUpdateViewModel Model) : IRequest<Operation<EventItemViewModel, string>>;
}