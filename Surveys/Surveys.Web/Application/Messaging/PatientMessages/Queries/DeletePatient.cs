using AutoMapper;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Base;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.PatientMessages.Queries;

public sealed class DeletePatient
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<PatientViewModel, string>>
    {
        public async Task<Operation<PatientViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<Patient> repository = unitOfWork.GetRepository<Patient>();
            Patient? entity = await repository.FindAsync([request.Id], cancellationToken);

            if (entity == null)
                return Operation.Error("Entity not found");

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            if (unitOfWork.LastSaveChangesResult.IsOk == false)
                return Operation.Error(unitOfWork.LastSaveChangesResult.Exception?.Message ?? AppData.Exceptions.SomethingWrong);

            PatientViewModel? mapped = mapper.Map<PatientViewModel>(entity);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<PatientViewModel, string>>;
}