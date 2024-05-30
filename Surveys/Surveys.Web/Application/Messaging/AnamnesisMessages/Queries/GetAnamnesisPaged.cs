using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.AnamnesisMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

public sealed class GetAnamnesisPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<AnamnesisViewModel>, string>>
    {
        public async Task<Operation<IPagedList<AnamnesisViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Anamnesis, bool>> predicate = GetPredicate(request.Search);

            IPagedList<Anamnesis> pagedList = await unitOfWork.GetRepository<Anamnesis>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<Anamnesis>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);

            IPagedList<AnamnesisViewModel>? mapped = mapper.Map<IPagedList<AnamnesisViewModel>>(pagedList);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }

        private Expression<Func<Anamnesis, bool>> GetPredicate(string? search)
        {
            Expression<Func<Anamnesis, bool>>? predicate = PredicateBuilder.True<Anamnesis>();

            if (search is null)
                return predicate;

            // predicate = predicate.And(x => x.Name.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<AnamnesisViewModel>, string>>;
}