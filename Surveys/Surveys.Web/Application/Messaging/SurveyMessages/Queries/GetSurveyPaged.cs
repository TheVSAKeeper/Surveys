using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.Queries;

public sealed class GetSurveyPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<SurveyViewModel>, string>>
    {
        public async Task<Operation<IPagedList<SurveyViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Survey, bool>> predicate = GetPredicate(request.Search, request.PatientId);

            IPagedList<Survey> pagedList = await unitOfWork.GetRepository<Survey>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<Survey>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);

            IPagedList<SurveyViewModel>? mapped = mapper.Map<IPagedList<SurveyViewModel>>(pagedList);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }

        private Expression<Func<Survey, bool>> GetPredicate(string? search, Guid? patientId)
        {
            Expression<Func<Survey, bool>>? predicate = PredicateBuilder.True<Survey>();

            if (patientId is not null)
                predicate = predicate.And(x => x.PatientId == patientId);

            if (search is null)
                return predicate;

            predicate = predicate.And(x => x.Complaint.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search, Guid? PatientId) : IRequest<Operation<IPagedList<SurveyViewModel>, string>>;
}