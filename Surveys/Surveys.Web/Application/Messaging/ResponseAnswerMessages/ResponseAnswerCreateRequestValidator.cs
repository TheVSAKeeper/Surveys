using FluentValidation;
using Surveys.Web.Application.Messaging.ResponseAnswerMessages.Queries;

namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages;

public class ResponseAnswerCreateRequestValidator : AbstractValidator<CreateResponseAnswer.Request>
{
    public ResponseAnswerCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}