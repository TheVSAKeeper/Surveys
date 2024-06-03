using Surveys.Blazor.Endpoints.PatientEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class SurveyUpdateViewModel
{
    public Guid Id { get; set; }

    public required string Complaint { get; set; }
    public required PatientViewModel Patient { get; set; }
    public required SurveyStatus Status { get; set; }

    public required List<AnamnesisUpdateViewModel>? Anamneses { get; set; }
}

public enum SurveyStatus
{
    None,
    Draft,
    Active,
    Closed
}