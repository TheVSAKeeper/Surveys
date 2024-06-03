using Surveys.Blazor.Endpoints.PatientEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class SurveyViewModel
{
    public Guid Id { get; set; }

    public required string Complaint { get; set; }
    public required PatientViewModel? Patient { get; set; }

    public SurveyStatus Status { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public List<AnamnesisViewModel>? Anamneses { get; set; }
}