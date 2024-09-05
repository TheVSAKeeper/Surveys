using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionOptionMessages.Queries;

public sealed class CreateQuestionOption
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<QuestionOptionViewModel, string>>
    {
        public async Task<Operation<QuestionOptionViewModel, string>> Handle(Request questionOptionRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new QuestionOption");

            QuestionOption? entity = mapper.Map<QuestionOptionCreateViewModel, QuestionOption>(questionOptionRequest.Model);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<QuestionOption>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                QuestionOptionViewModel? mapped = mapper.Map<QuestionOption, QuestionOptionViewModel>(entity);

                if (mapped is null)
                {
                    return Operation.Error(AppData.Exceptions.MappingException);
                }

                logger.LogInformation("New entity {@QuestionOption} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(QuestionOptionCreateViewModel Model) : IRequest<Operation<QuestionOptionViewModel, string>>;
}
