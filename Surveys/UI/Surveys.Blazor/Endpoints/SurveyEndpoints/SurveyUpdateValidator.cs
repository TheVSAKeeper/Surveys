using FluentValidation;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints;

public class SurveyUpdateValidator : AbstractValidator<SurveyUpdateViewModel>
{
    public SurveyUpdateValidator()
    {
        RuleFor(viewModel => viewModel.Complaint)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(1024)
            .WithName("Жалоба");
    }
}
