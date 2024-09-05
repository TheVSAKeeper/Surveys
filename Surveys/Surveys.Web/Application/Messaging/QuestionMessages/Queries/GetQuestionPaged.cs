using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionMessages.Queries;

public sealed class GetQuestionPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<QuestionViewModel>, string>>
    {
        public async Task<Operation<IPagedList<QuestionViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Question, bool>> predicate = GetPredicate(request.Search);

            IPagedList<Question> pagedList = await unitOfWork.GetRepository<Question>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    orderBy: o => o.OrderBy(x => x.SortIndex),
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
            {
                pagedList = await unitOfWork.GetRepository<Question>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize,
                        orderBy: o => o.OrderBy(x => x.SortIndex),
                        cancellationToken: cancellationToken);
            }

            IPagedList<QuestionViewModel>? mapped = mapper.Map<IPagedList<QuestionViewModel>>(pagedList);

            if (mapped is null)
            {
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            return Operation.Result(mapped);
        }

        private Expression<Func<Question, bool>> GetPredicate(string? search)
        {
            Expression<Func<Question, bool>>? predicate = PredicateBuilder.True<Question>();

            if (search is null)
            {
                return predicate;
            }

            // predicate = predicate.And(x => x.Name.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<QuestionViewModel>, string>>;
}
