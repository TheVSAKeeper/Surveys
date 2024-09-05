using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Surveys.Web.Pages.Connect;

public class LoginViewModel
{
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; } = null!;

    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;

    [Required]
    public string ReturnUrl { get; set; } = null!;
}

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(model => model.UserName)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithName("Имя пользователя");

        RuleFor(model => model.Password)
            .NotNull()
            .NotEmpty()
            .WithName("Пароль");
    }
}
