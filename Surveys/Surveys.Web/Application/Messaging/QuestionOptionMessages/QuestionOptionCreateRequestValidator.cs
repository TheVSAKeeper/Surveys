using FluentValidation;
using Surveys.Web.Application.Messaging.QuestionOptionMessages.Queries;

namespace Surveys.Web.Application.Messaging.QuestionOptionMessages;

public class QuestionOptionCreateRequestValidator : AbstractValidator<CreateQuestionOption.Request>
{
    public QuestionOptionCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}
