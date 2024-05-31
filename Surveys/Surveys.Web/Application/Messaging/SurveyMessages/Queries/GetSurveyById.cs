using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.Queries;

public sealed class GetSurveyById
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<SurveyViewModel, string>>
    {
        public async Task<Operation<SurveyViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            IRepository<Survey> repository = unitOfWork.GetRepository<Survey>();
            Survey? entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: survey => survey.Id == id);

            if (entityWithoutIncludes == null)
                return Operation.Error($"Entity with identifier {id} not found");

            SurveyViewModel? mapped = mapper.Map<SurveyViewModel>(entityWithoutIncludes);

            if (mapped is null)
                return Operation.Error(AppData.Exceptions.MappingException);

            return Operation.Result(mapped);
        }
    }

    public record Request(Guid Id) : IRequest<Operation<SurveyViewModel, string>>;
}