using Surveys.Domain.Base;

namespace Surveys.Domain;

public class SurveyDiagnosis : Auditable
{
    public string Reason { get; set; } = null!;

    public Guid DiagnosisId { get; set; }
    public virtual Diagnosis? Diagnosis { get; set; }

    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; } = null!;
    
    public Guid SurveyId { get; set; }
    public virtual Survey Survey { get; set; } = null!;
}