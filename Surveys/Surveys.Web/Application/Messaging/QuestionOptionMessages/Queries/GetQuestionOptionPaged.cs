using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionOptionMessages.Queries;

public sealed class GetQuestionOptionPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<QuestionOptionViewModel>, string>>
    {
        public async Task<Operation<IPagedList<QuestionOptionViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<QuestionOption, bool>> predicate = GetPredicate(request.Search);

            IPagedList<QuestionOption> pagedList = await unitOfWork.GetRepository<QuestionOption>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    orderBy: o => o.OrderBy(x => x.SortIndex),
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
            {
                pagedList = await unitOfWork.GetRepository<QuestionOption>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize,
                        orderBy: o => o.OrderBy(x => x.SortIndex),
                        cancellationToken: cancellationToken);
            }

            IPagedList<QuestionOptionViewModel>? mapped = mapper.Map<IPagedList<QuestionOptionViewModel>>(pagedList);

            if (mapped is null)
            {
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            return Operation.Result(mapped);
        }

        private Expression<Func<QuestionOption, bool>> GetPredicate(string? search)
        {
            Expression<Func<QuestionOption, bool>>? predicate = PredicateBuilder.True<QuestionOption>();

            if (search is null)
            {
                return predicate;
            }

            //predicate = predicate.And(x => x.Name.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<QuestionOptionViewModel>, string>>;
}
