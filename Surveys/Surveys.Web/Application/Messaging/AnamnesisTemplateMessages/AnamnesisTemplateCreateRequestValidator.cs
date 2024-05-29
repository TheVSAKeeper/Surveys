using FluentValidation;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.Queries;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages;

public class AnamnesisTemplateCreateRequestValidator : AbstractValidator<CreateAnamnesisTemplate.Request>
{
    public AnamnesisTemplateCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}