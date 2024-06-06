using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

public sealed class CreateAnamnesis
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<AnamnesisViewModel, string>>
    {
        public async Task<Operation<AnamnesisViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new Anamnesis");

            Anamnesis? entity = mapper.Map<AnamnesisCreateViewModel, Anamnesis>(request.Model,
                options => options.Items[nameof(ApplicationUser)] = request.User.Identity!.Name);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            AnamnesisTemplate? template = await unitOfWork.GetRepository<AnamnesisTemplate>()
                    .GetFirstOrDefaultAsync(predicate: p => p.Id == request.Model.AnamnesisTemplateId,
                        include: i => i.Include(x => x.Questions))
                ;

            if (template == null)
            {
                logger.LogError("Template of anamnesis not found");
                return Operation.Error($"Entity with identifier {request.Model.AnamnesisTemplateId} not found");
            }

            List<Response> responses = [];

            responses.AddRange(template.Questions.Select(question => new Response
            {
                AnamnesisId = entity.Id,
                QuestionId = question.Id,
                Answers = []
            }));

            entity.Responses = responses;

            await unitOfWork.GetRepository<Anamnesis>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk == false)
            {
                string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
                logger.LogError(errorMessage);
                return Operation.Error(errorMessage);
            }

            AnamnesisViewModel? mapped = mapper.Map<Anamnesis, AnamnesisViewModel>(entity);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            //logger.LogInformation("New entity {@Anamnesis} successfully created", entity);
            return Operation.Result(mapped);
        }
    }

    public record Request(AnamnesisCreateViewModel Model, ClaimsPrincipal User) : IRequest<Operation<AnamnesisViewModel, string>>;
}