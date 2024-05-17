using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Survey : Auditable
{
    public required string Complaint { get; set; }

    public required Guid PatientId { get; set; }
    public virtual Patient? Patient { get; set; }

    public bool IsComplete { get; set; }

    public virtual IList<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
    public virtual IList<Anamnesis>? Anamneses { get; set; }
}