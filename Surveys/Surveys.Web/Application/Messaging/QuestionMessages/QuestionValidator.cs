using FluentValidation;
using Surveys.Web.Application.Messaging.QuestionMessages.Queries;

namespace Surveys.Web.Application.Messaging.QuestionMessages;

/// <summary>
///     RegisterViewModel Validator
/// </summary>
public class QuestionCreateRequestValidator : AbstractValidator<CreateQuestion.Request>
{
    public QuestionCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            RuleFor(request => request.Model.Content).NotEmpty().MaximumLength(4000);
            RuleFor(request => request.Model.SortIndex).GreaterThan(-1);
        });
    }
}