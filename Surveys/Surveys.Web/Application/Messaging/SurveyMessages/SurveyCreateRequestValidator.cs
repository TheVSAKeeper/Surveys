using FluentValidation;
using Surveys.Web.Application.Messaging.SurveyMessages.Queries;

namespace Surveys.Web.Application.Messaging.SurveyMessages;

public class SurveyCreateRequestValidator : AbstractValidator<CreateSurvey.Request>
{
    public SurveyCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}
