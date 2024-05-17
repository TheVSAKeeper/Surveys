using FluentValidation;
using Surveys.Web.Application.Messaging.PatientMessages.Queries;

namespace Surveys.Web.Application.Messaging.PatientMessages;

public class PatientCreateRequestValidator : AbstractValidator<CreatePatient.Request>
{
    public PatientCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            RuleFor(x => x.Model.Gender).NotNull();
            RuleFor(x => x.Model.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Model.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Model.Patronymic).MaximumLength(50);
            RuleFor(x => x.Model.BirthDate).NotEmpty();
        });
    }
}