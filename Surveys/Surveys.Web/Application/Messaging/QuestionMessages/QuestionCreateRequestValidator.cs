using FluentValidation;
using Surveys.Web.Application.Messaging.QuestionMessages.Queries;

namespace Surveys.Web.Application.Messaging.QuestionMessages;

public class QuestionCreateRequestValidator : AbstractValidator<CreateQuestion.Request>
{
    public QuestionCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}
