using FluentValidation;

namespace Surveys.WPF.Features.Authentication.Update;

public class ApplicationUserUpdateDtoValidator : AbstractValidator<ApplicationUserUpdateDto>
{
    public ApplicationUserUpdateDtoValidator()
    {
        RuleFor(user => user.DisplayName)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(30);

        RuleFor(user => user.UserName)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(20);

        RuleFor(user => user.FirstName)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(30);

        RuleFor(user => user.LastName)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(30);

        RuleFor(user => user.Patronymic)
            .MinimumLength(6)
            .MaximumLength(30);
    }
}