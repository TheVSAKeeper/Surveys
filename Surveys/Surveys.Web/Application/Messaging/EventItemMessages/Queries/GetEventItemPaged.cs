using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.EventItemMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
///     Request for paged list of EventItems
/// </summary>
public sealed class GetEventItemPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<EventItemViewModel>, string>>
    {
        public async Task<Operation<IPagedList<EventItemViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<EventItem, bool>> predicate = GetPredicate(request.Search);

            IPagedList<EventItem> pagedList = await unitOfWork.GetRepository<EventItem>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<EventItem>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);

            IPagedList<EventItemViewModel>? mapped = mapper.Map<IPagedList<EventItemViewModel>>(pagedList);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }

        private Expression<Func<EventItem, bool>> GetPredicate(string? search)
        {
            Expression<Func<EventItem, bool>>? predicate = PredicateBuilder.True<EventItem>();

            if (search is null)
                return predicate;

            predicate = predicate.And(x => x.Message.Contains(search));
            predicate = predicate.Or(x => x.Logger.Contains(search));
            predicate = predicate.Or(x => x.Level.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<EventItemViewModel>, string>>;
}