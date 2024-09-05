using System.Security.Claims;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.Queries;

public sealed class UpdateSurvey
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<SurveyViewModel, string>>
    {
        public async Task<Operation<SurveyViewModel, string>> Handle(Request surveyRequest, CancellationToken cancellationToken)
        {
            IRepository<Survey> repository = unitOfWork.GetRepository<Survey>();

            Survey? entity = await repository.GetFirstOrDefaultAsync(predicate: survey => survey.Id == surveyRequest.Id, disableTracking: false);

            // TODO: !!Anamneses

            if (entity == null)
            {
                return Operation.Error(AppData.Exceptions.NotFoundException);
            }

            mapper.Map(surveyRequest.Model, entity, options => options.Items[nameof(ApplicationUser)] = surveyRequest.User.Identity!.Name);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                SurveyViewModel? mapped = mapper.Map<Survey, SurveyViewModel>(entity);

                if (mapped is not null)
                {
                    return Operation.Result(mapped);
                }

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, SurveyUpdateViewModel Model, ClaimsPrincipal User) : IRequest<Operation<SurveyViewModel, string>>;
}
