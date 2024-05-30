using Surveys.Web.Application.Messaging.AnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnswerMessages.Queries;

public sealed class CreateAnswer
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<AnswerViewModel, string>>
    {
        public async Task<Operation<AnswerViewModel, string>> Handle(Request AnswerRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new Answer");

            Answer? entity = mapper.Map<AnswerCreateViewModel, Answer>(AnswerRequest.Model);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<Answer>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                AnswerViewModel? mapped = mapper.Map<Answer, AnswerViewModel>(entity);

                if (mapped is null)
                    return Operation.Error(AppData.Exceptions.MappingException);

                logger.LogInformation("New entity {@Answer} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(AnswerCreateViewModel Model) : IRequest<Operation<AnswerViewModel, string>>;
}