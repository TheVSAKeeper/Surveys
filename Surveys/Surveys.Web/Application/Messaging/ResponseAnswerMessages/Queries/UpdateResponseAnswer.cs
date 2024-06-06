using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.Queries;

public sealed class UpdateResponseAnswer
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<ResponseAnswerViewModel, string>>
    {
        public async Task<Operation<ResponseAnswerViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<ResponseAnswer> repository = unitOfWork.GetRepository<ResponseAnswer>();

            ResponseAnswer? entity = await repository.GetFirstOrDefaultAsync(predicate: responseAnswer => responseAnswer.Id == request.Id, disableTracking: false);

            if (entity == null)
                return Operation.Error(AppData.Exceptions.NotFoundException);

            mapper.Map(request.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk == false)
            {
                string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
                return Operation.Error(errorMessage);
            }

            ResponseAnswerViewModel? mapped = mapper.Map<ResponseAnswer, ResponseAnswerViewModel>(entity);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id, ResponseAnswerUpdateViewModel Model) : IRequest<Operation<ResponseAnswerViewModel, string>>;
}