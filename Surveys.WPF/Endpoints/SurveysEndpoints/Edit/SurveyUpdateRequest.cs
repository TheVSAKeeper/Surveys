using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Exceptions;
using Surveys.Infrastructure;
using Surveys.WPF.Endpoints.AuthenticationEndpoints;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public record SurveyUpdateRequest(SurveyEditDto Model) : IRequest<OperationResult<Guid>>;

public class SurveyUpdateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, AuthenticationStore authenticationStore) : IRequestHandler<SurveyUpdateRequest, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(SurveyUpdateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<Guid> operation = OperationResult.CreateResult<Guid>();
        IRepository<Survey> repository = unitOfWork.GetRepository<Survey>();

        Survey? entity = await repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Model.Id,
            disableTracking: false);

        if (entity is null)
        {
            operation.AddError(new SurveysNotFoundException(nameof(Survey), request.Model.Id.ToString()));
            return operation;
        }

        mapper.Map(request.Model, entity, options => options.Items[nameof(ApplicationUser)] = authenticationStore.Username);

        if (request.Model.Anamneses!.All(x=>x.IsComplete))
            entity.IsComplete = true;

        repository.Update(entity);

        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            Exception exception = unitOfWork.LastSaveChangesResult.Exception
                                  ?? new SurveysDatabaseSaveException(nameof(Survey));

            operation.AddError(exception);
            return operation;
        }

        operation.Result = entity.Id;

        return operation;
    }
}