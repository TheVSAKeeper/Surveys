using AutoMapper;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Base;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.Queries;

public sealed class DeleteAnamnesisTemplate
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnamnesisTemplateViewModel, string>>
    {
        public async Task<Operation<AnamnesisTemplateViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<AnamnesisTemplate> repository = unitOfWork.GetRepository<AnamnesisTemplate>();
            AnamnesisTemplate? entity = await repository.FindAsync([request.Id], cancellationToken);

            if (entity == null)
                return Operation.Error("Entity not found");

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            if (unitOfWork.LastSaveChangesResult.IsOk == false)
                return Operation.Error(unitOfWork.LastSaveChangesResult.Exception?.Message ?? AppData.Exceptions.SomethingWrong);

            AnamnesisTemplateViewModel? mapped = mapper.Map<AnamnesisTemplateViewModel>(entity);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<AnamnesisTemplateViewModel, string>>;
}