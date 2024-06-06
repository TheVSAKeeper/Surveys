using System.Security.Claims;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.Queries;

public sealed class CreateSurvey
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<SurveyViewModel, string>>
    {
        public async Task<Operation<SurveyViewModel, string>> Handle(Request surveyRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new Survey");

            Survey? entity = mapper.Map<SurveyCreateViewModel, Survey>(surveyRequest.Model,
                options => options.Items[nameof(ApplicationUser)] = surveyRequest.User.Identity!.Name);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<Survey>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                SurveyViewModel? mapped = mapper.Map<Survey, SurveyViewModel>(entity);

                if (mapped is null)
                    return Operation.Error(AppData.Exceptions.MappingException);

                logger.LogInformation("New entity {@Survey} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(SurveyCreateViewModel Model, ClaimsPrincipal User, bool? IsDefault) : IRequest<Operation<SurveyViewModel, string>>;
}