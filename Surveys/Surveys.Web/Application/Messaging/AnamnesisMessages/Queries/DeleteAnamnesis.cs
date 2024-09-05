namespace Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

public sealed class DeleteAnamnesis
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnamnesisViewModel, string>>
    {
        public async Task<Operation<AnamnesisViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();
            Anamnesis? entity = await repository.FindAsync([request.Id], cancellationToken);

            if (entity == null)
            {
                return Operation.Error("Entity not found");
            }

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            if (unitOfWork.LastSaveChangesResult.IsOk == false)
            {
                return Operation.Error(unitOfWork.LastSaveChangesResult.Exception?.Message ?? AppData.Exceptions.SomethingWrong);
            }

            AnamnesisViewModel? mapped = mapper.Map<AnamnesisViewModel>(entity);

            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<AnamnesisViewModel, string>>;
}
