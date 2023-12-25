using System.Windows;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Infrastructure;
using Surveys.WPF.Exceptions;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Authentication.EditProfile;

public class EditProfileCommand(ProfileDetailsViewModel profileDetailsViewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<Guid> result = await mediator.Send(new UserUpdateRequest(profileDetailsViewModel.User));

        if (result.Ok)
            MessageBox.Show($"Пользователь {result.Result} обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
        else
            MessageBox.Show("Ошибка обновления пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

public class UserUpdateViewModel
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? DisplayName { get; set; }
}

public record UserUpdateRequest(UserUpdateViewModel Model) : IRequest<OperationResult<Guid>>;

public class UserUpdateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UserUpdateRequest, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
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

      var a =  mapper.Map(request.Model, entity);

        repository.Update(a);

        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            Exception exception = unitOfWork.LastSaveChangesResult.Exception
                                  ?? new SurveysDatabaseSaveException("Error saving entity Product");

            operation.AddError(exception);
            return operation;
        }

        // var result = _mapper.Map<ProductViewModel>(entity);

        operation.Result = entity.Id;

        return operation;
    }
}