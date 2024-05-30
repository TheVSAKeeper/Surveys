using FluentValidation;
using Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages;

public class AnamnesisCreateRequestValidator : AbstractValidator<CreateAnamnesis.Request>
{
    public AnamnesisCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}