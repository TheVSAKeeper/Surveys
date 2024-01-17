using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Diagnosis : Identity
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
}