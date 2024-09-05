using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.Queries;

public sealed class UpdateAnamnesisTemplate
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnamnesisTemplateViewModel, string>>
    {
        public async Task<Operation<AnamnesisTemplateViewModel, string>> Handle(Request anamnesisTemplateRequest, CancellationToken cancellationToken)
        {
            IRepository<AnamnesisTemplate> repository = unitOfWork.GetRepository<AnamnesisTemplate>();

            AnamnesisTemplate? entity = await repository.GetFirstOrDefaultAsync(predicate: anamnesisTemplate => anamnesisTemplate.Id == anamnesisTemplateRequest.Id, disableTracking: false);

            if (entity == null)
            {
                return Operation.Error(AppData.Exceptions.NotFoundException);
            }

            mapper.Map(anamnesisTemplateRequest.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                AnamnesisTemplateViewModel? mapped = mapper.Map<AnamnesisTemplate, AnamnesisTemplateViewModel>(entity);

                if (mapped is not null)
                {
                    return Operation.Result(mapped);
                }

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, AnamnesisTemplateUpdateViewModel Model) : IRequest<Operation<AnamnesisTemplateViewModel, string>>;
}
