using FluentValidation;
using Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints;

public class SurveyCreateValidator : AbstractValidator<SurveyCreateViewModel>
{
    public SurveyCreateValidator()
    {
        RuleFor(viewModel => viewModel.Complaint)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(1024)
            .WithName("Жалоба");

        RuleFor(viewModel => viewModel.PatientId)
            .NotNull();
    }
}

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