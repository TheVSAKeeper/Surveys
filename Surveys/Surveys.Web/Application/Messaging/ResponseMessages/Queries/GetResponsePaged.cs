using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.Queries;

public sealed class GetResponsePaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<ResponseViewModel>, string>>
    {
        public async Task<Operation<IPagedList<ResponseViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Response, bool>> predicate = GetPredicate(request.Search);

            IPagedList<Response> pagedList = await unitOfWork.GetRepository<Response>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<Response>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);

            IPagedList<ResponseViewModel>? mapped = mapper.Map<IPagedList<ResponseViewModel>>(pagedList);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }

        private Expression<Func<Response, bool>> GetPredicate(string? search)
        {
            Expression<Func<Response, bool>>? predicate = PredicateBuilder.True<Response>();

            if (search is null)
                return predicate;

            //predicate = predicate.And(x => x.Name.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<ResponseViewModel>, string>>;
}