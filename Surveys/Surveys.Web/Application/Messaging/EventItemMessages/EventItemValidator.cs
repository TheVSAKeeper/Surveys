using FluentValidation;
using Surveys.Web.Application.Messaging.EventItemMessages.Queries;

namespace Surveys.Web.Application.Messaging.EventItemMessages;

/// <summary>
///     RegisterViewModel Validator
/// </summary>
public class EventItemCreateRequestValidator : AbstractValidator<CreateEventItem.Request>
{
    public EventItemCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            RuleFor(request => request.Model.CreatedAt).NotNull();
            RuleFor(request => request.Model.Message).NotEmpty().MaximumLength(4000);
            RuleFor(request => request.Model.Level).NotEmpty().MaximumLength(50);
            RuleFor(request => request.Model.Logger).NotEmpty().MaximumLength(255);
            RuleFor(request => request.Model.ThreadId).MaximumLength(50);
            RuleFor(request => request.Model.ExceptionMessage).MaximumLength(4000);
        });
    }
}