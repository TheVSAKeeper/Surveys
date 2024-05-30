using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.AnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnswerMessages.Queries;

public sealed class GetAnswerPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<AnswerViewModel>, string>>
    {
        public async Task<Operation<IPagedList<AnswerViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Answer, bool>> predicate = GetPredicate(request.Search);

            IPagedList<Answer> pagedList = await unitOfWork.GetRepository<Answer>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<Answer>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);

            IPagedList<AnswerViewModel>? mapped = mapper.Map<IPagedList<AnswerViewModel>>(pagedList);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }

        private Expression<Func<Answer, bool>> GetPredicate(string? search)
        {
            Expression<Func<Answer, bool>>? predicate = PredicateBuilder.True<Answer>();

            if (search is null)
                return predicate;

            // predicate = predicate.And(x => x.Name.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<AnswerViewModel>, string>>;
}