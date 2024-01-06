using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain.Exceptions;
using Surveys.Infrastructure;

namespace Surveys.WPF.Features.Authentication.Update;

public record ApplicationUserUpdateRequest(ApplicationUserUpdateDto Model) : IRequest<OperationResult<Guid>>;

public class ApplicationUserUpdateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ApplicationUserUpdateRequest, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(ApplicationUserUpdateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<Guid> operation = OperationResult.CreateResult<Guid>();
        IRepository<ApplicationUser> repository = unitOfWork.GetRepository<ApplicationUser>();

        ApplicationUser? entity = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Model.Id,
            disableTracking: false);

        if (entity is null)
        {
            operation.AddError(new SurveysNotFoundException(nameof(ApplicationUser), request.Model.Id.ToString()));
            return operation;
        }

        mapper.Map(request.Model, entity);

        repository.Update(entity);

        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            Exception exception = unitOfWork.LastSaveChangesResult.Exception
                                  ?? new SurveysDatabaseSaveException(nameof(ApplicationUser));

            operation.AddError(exception);
            return operation;
        }

        operation.Result = entity.Id;

        return operation;
    }
}