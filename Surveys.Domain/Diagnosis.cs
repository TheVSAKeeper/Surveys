using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Diagnosis : Identity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
}