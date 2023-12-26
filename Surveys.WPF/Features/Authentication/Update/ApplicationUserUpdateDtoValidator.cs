using FluentValidation;

namespace Surveys.WPF.Features.Authentication.Update
{
    public class ApplicationUserUpdateDtoValidator : AbstractValidator<ApplicationUserUpdateDto>
    {
        public ApplicationUserUpdateDtoValidator()
        {
            RuleFor(a => a.DisplayName)
                .NotEmpty().MinimumLength(10).MaximumLength(30);
        }
    }
}
