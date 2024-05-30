using FluentValidation;
using Surveys.Web.Application.Messaging.AnswerMessages.Queries;

namespace Surveys.Web.Application.Messaging.AnswerMessages;

public class AnswerCreateRequestValidator : AbstractValidator<CreateAnswer.Request>
{
    public AnswerCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}