using FluentValidation;
using Surveys.Web.Application.Messaging.ResponseMessages.Queries;

namespace Surveys.Web.Application.Messaging.ResponseMessages;

public class ResponseCreateRequestValidator : AbstractValidator<CreateResponse.Request>
{
    public ResponseCreateRequestValidator()
    {
        RuleSet("default", () =>
        {
            //  RuleFor(request => request.Model.CreatedAt).NotNull();
        });
    }
}
