using AutoMapper;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Base;
using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionMessages.Queries;

/// <summary>
///     Question by Identifier
/// </summary>
public sealed class GetQuestionById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<QuestionViewModel, string>>
    {
        /// <summary>Handles a request getting log by identifier</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<Operation<QuestionViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Question> repository = unitOfWork.GetRepository<Question>();
            Question? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: Question => Question.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            QuestionViewModel? mapped = mapper.Map<QuestionViewModel>(entityWithoutIncludes);

            if (mapped is not null)
                return Operation.Result(mapped);

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<QuestionViewModel, string>>;
}