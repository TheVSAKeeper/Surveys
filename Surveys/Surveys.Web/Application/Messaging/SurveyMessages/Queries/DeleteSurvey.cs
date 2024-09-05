using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.Queries;

public sealed class DeleteSurvey
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<SurveyViewModel, string>>
    {
        public async Task<Operation<SurveyViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            IRepository<Survey> repository = unitOfWork.GetRepository<Survey>();
            Survey? entity = await repository.FindAsync([request.Id], cancellationToken);

            if (entity == null)
            {
                return Operation.Error("Entity not found");
            }

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            if (unitOfWork.LastSaveChangesResult.IsOk == false)
            {
                return Operation.Error(unitOfWork.LastSaveChangesResult.Exception?.Message ?? AppData.Exceptions.SomethingWrong);
            }

            SurveyViewModel? mapped = mapper.Map<SurveyViewModel>(entity);

            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<SurveyViewModel, string>>;
}
