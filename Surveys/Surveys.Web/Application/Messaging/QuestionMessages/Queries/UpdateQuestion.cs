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
///     Request: Question edit
/// </summary>
public sealed class UpdateQuestion
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<QuestionViewModel, string>>
    {
        public async Task<Operation<QuestionViewModel, string>> Handle(Request QuestionRequest, CancellationToken cancellationToken)
        {
            IRepository<Question> repository = unitOfWork.GetRepository<Question>();

            Question? entity = await repository.GetFirstOrDefaultAsync(predicate: Question => Question.Id == QuestionRequest.Id, disableTracking: false);

            if (entity == null)
                return Operation.Error(AppContracts.Exceptions.NotFoundException);

            mapper.Map(QuestionRequest.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                QuestionViewModel? mapped = mapper.Map<Question, QuestionViewModel>(entity);

                if (mapped is not null)
                    return Operation.Result(mapped);

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, QuestionUpdateViewModel Model) : IRequest<Operation<QuestionViewModel, string>>;
}