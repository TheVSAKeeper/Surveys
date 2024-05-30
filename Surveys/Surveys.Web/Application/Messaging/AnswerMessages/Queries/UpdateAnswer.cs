using Surveys.Web.Application.Messaging.AnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnswerMessages.Queries;

public sealed class UpdateAnswer
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<AnswerViewModel, string>>
    {
        public async Task<Operation<AnswerViewModel, string>> Handle(Request AnswerRequest, CancellationToken cancellationToken)
        {
            IRepository<Answer> repository = unitOfWork.GetRepository<Answer>();

            Answer? entity = await repository.GetFirstOrDefaultAsync(predicate: Answer => Answer.Id == AnswerRequest.Id, disableTracking: false);

            if (entity == null)
                return Operation.Error(AppData.Exceptions.NotFoundException);

            mapper.Map(AnswerRequest.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                AnswerViewModel? mapped = mapper.Map<Answer, AnswerViewModel>(entity);

                if (mapped is not null)
                    return Operation.Result(mapped);

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, AnswerUpdateViewModel Model) : IRequest<Operation<AnswerViewModel, string>>;
}