using Surveys.Domain;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

public class SurveyShowDto
{
    public required Guid Id { get; init; }

    public required string Complaint { get; set; }
    public required Patient Patient { get; set; }

    public required bool IsComplete { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}