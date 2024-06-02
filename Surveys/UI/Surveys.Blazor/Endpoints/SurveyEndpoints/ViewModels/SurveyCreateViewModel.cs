using Surveys.Blazor.Endpoints.PatientEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class SurveyCreateViewModel
{
    public Guid Id { get; set; }

    public required Guid PatientId { get; set; }
    public PatientViewModel? Patient { get; set; }
    public required string Complaint { get; set; }
}