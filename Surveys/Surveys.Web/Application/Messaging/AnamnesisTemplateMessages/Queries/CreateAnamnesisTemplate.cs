using AutoMapper;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Base;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.Queries;

public sealed class CreateAnamnesisTemplate
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<AnamnesisTemplateViewModel, string>>
    {
        public async Task<Operation<AnamnesisTemplateViewModel, string>> Handle(Request anamnesisTemplateRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new AnamnesisTemplate");

            AnamnesisTemplate? entity = mapper.Map<AnamnesisTemplateCreateViewModel, AnamnesisTemplate>(anamnesisTemplateRequest.Model);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<AnamnesisTemplate>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                AnamnesisTemplateViewModel? mapped = mapper.Map<AnamnesisTemplate, AnamnesisTemplateViewModel>(entity);

                if (mapped is null)
                    return Operation.Error(AppData.Exceptions.MappingException);

                logger.LogInformation("New entity {@AnamnesisTemplate} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(AnamnesisTemplateCreateViewModel Model) : IRequest<Operation<AnamnesisTemplateViewModel, string>>;
}