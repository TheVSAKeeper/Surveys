using AutoMapper;
using Calabonga.Microservices.Core;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Base;
using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionMessages.Queries;

/// <summary>
///     Request: Question creation
/// </summary>
public sealed class CreateQuestion
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<QuestionViewModel, string>>
    {
        public async Task<Operation<QuestionViewModel, string>> Handle(Request QuestionRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new Question");

            Question? entity = mapper.Map<QuestionCreateViewModel, Question>(QuestionRequest.Model);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppContracts.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<Question>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                QuestionViewModel? mapped = mapper.Map<Question, QuestionViewModel>(entity);

                if (mapped is null)
                    return Operation.Error(AppData.Exceptions.MappingException);

                logger.LogInformation("New entity {@Question} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(QuestionCreateViewModel Model) : IRequest<Operation<QuestionViewModel, string>>;
}