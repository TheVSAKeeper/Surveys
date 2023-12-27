using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Survey : Auditable
{
    public string Complaint { get; set; } = null!;

    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; } = null!;

    public bool IsComplete { get; set; }

    public virtual ICollection<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
    public virtual ICollection<Anamnesis>? Anamneses { get; set; }
}