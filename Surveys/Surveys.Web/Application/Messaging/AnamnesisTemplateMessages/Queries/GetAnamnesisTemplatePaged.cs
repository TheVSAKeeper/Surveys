using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.Queries;

public sealed class GetAnamnesisTemplatePaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<AnamnesisTemplateViewModel>, string>>
    {
        public async Task<Operation<IPagedList<AnamnesisTemplateViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<AnamnesisTemplate, bool>> predicate = GetPredicate(request.Search);

            IPagedList<AnamnesisTemplate> pagedList = await unitOfWork.GetRepository<AnamnesisTemplate>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<AnamnesisTemplate>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);

            IPagedList<AnamnesisTemplateViewModel>? mapped = mapper.Map<IPagedList<AnamnesisTemplateViewModel>>(pagedList);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }

        private Expression<Func<AnamnesisTemplate, bool>> GetPredicate(string? search)
        {
            Expression<Func<AnamnesisTemplate, bool>>? predicate = PredicateBuilder.True<AnamnesisTemplate>();

            if (search is null)
                return predicate;

            // predicate = predicate.And(x => x.Name.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<AnamnesisTemplateViewModel>, string>>;
}