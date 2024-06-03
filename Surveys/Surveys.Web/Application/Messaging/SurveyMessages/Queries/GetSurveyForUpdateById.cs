using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.Queries;

public sealed class GetSurveyForUpdateById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<SurveyUpdateViewModel, string>>
    {
        public async Task<Operation<SurveyUpdateViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Survey> repository = unitOfWork.GetRepository<Survey>();
            Survey? entity = await repository.GetFirstOrDefaultAsync(predicate: survey => survey.Id == id);

            if (entity == null)
                return Operation.Error($"Entity with identifier {id} not found");

            SurveyUpdateViewModel? mapped = mapper.Map<SurveyUpdateViewModel>(entity);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<SurveyUpdateViewModel, string>>;
}