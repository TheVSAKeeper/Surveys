using System.Linq.Expressions;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.PatientMessages.Queries;

public sealed class GetPatientPaged
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<PatientViewModel>, string>>
    {
        public async Task<Operation<IPagedList<PatientViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Patient, bool>> predicate = GetPredicate(request.Search);

            IPagedList<Patient> pagedList = await unitOfWork.GetRepository<Patient>()
                .GetPagedListAsync(predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
                pagedList = await unitOfWork.GetRepository<Patient>()
                    .GetPagedListAsync(pageIndex: 0,
                        pageSize: request.PageSize,
                        cancellationToken: cancellationToken);

            IPagedList<PatientViewModel>? mapped = mapper.Map<IPagedList<PatientViewModel>>(pagedList);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }

        private Expression<Func<Patient, bool>> GetPredicate(string? search)
        {
            Expression<Func<Patient, bool>>? predicate = PredicateBuilder.True<Patient>();

            if (search is null)
                return predicate;

            predicate = predicate.And(patient => patient.FirstName.Contains(search));
            predicate = predicate.Or(patient => patient.LastName.Contains(search));
            predicate = predicate.Or(patient => patient.Patronymic != null && patient.Patronymic.Contains(search));
            return predicate;
        }
    }

    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<PatientViewModel>, string>>;
}