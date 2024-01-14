using Surveys.Domain;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyEditDto
{
    public required Guid Id { get; init; }

    public required string Complaint { get; set; }
    public required Patient Patient { get; set; }

    public required bool IsComplete { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }

    public required ICollection<Anamnesis>? Anamneses { get; set; }
}