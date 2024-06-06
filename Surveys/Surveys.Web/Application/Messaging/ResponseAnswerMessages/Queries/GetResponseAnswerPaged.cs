using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.Queries;

public sealed class GetResponseAnswerPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<ResponseAnswerViewModel>, string>>
    {
        public async Task<Operation<IPagedList<ResponseAnswerViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<ResponseAnswer, bool>> predicate = GetPredicate(request.Search);

            IPagedList<ResponseAnswer> pagedList = await unitOfWork.GetRepository<ResponseAnswer>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<ResponseAnswer>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);

            IPagedList<ResponseAnswerViewModel>? mapped = mapper.Map<IPagedList<ResponseAnswerViewModel>>(pagedList);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }

        private Expression<Func<ResponseAnswer, bool>> GetPredicate(string? search)
        {
            Expression<Func<ResponseAnswer, bool>>? predicate = PredicateBuilder.True<ResponseAnswer>();

            if (string.IsNullOrWhiteSpace(search))
                return predicate;

            predicate = predicate.And(x => x.ResponseId.ToString() == search);
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<ResponseAnswerViewModel>, string>>;
}