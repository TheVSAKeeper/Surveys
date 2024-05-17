using AutoMapper;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Base;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.PatientMessages.Queries;

public sealed class GetPatientById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<PatientViewModel, string>>
    {
        public async Task<Operation<PatientViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Patient> repository = unitOfWork.GetRepository<Patient>();
            Patient? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            PatientViewModel? mapped = mapper.Map<PatientViewModel>(entityWithoutIncludes);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<PatientViewModel, string>>;
}