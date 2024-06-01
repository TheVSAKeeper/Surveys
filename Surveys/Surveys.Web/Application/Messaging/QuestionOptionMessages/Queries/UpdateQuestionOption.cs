using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionOptionMessages.Queries;

public sealed class UpdateQuestionOption
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<QuestionOptionViewModel, string>>
    {
        public async Task<Operation<QuestionOptionViewModel, string>> Handle(Request questionOptionRequest, CancellationToken cancellationToken)
        {
            IRepository<QuestionOption> repository = unitOfWork.GetRepository<QuestionOption>();

            QuestionOption? entity = await repository.GetFirstOrDefaultAsync(predicate: questionOption => questionOption.Id == questionOptionRequest.Id, disableTracking: false);

            if (entity == null)
                return Operation.Error(AppData.Exceptions.NotFoundException);

            mapper.Map(questionOptionRequest.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                QuestionOptionViewModel? mapped = mapper.Map<QuestionOption, QuestionOptionViewModel>(entity);

                if (mapped is not null)
                    return Operation.Result(mapped);

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, QuestionOptionUpdateViewModel Model) : IRequest<Operation<QuestionOptionViewModel, string>>;
}