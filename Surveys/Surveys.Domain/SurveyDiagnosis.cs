using Surveys.Domain.Base;

namespace Surveys.Domain;

public class SurveyDiagnosis : Auditable
{
    public required string Reason { get; set; }

    public required Guid DiagnosisId { get; set; }
    public virtual Diagnosis? Diagnosis { get; set; }

    public required Guid PatientId { get; set; }
    public virtual Patient? Patient { get; set; }

    public required Guid SurveyId { get; set; }
    public virtual Survey? Survey { get; set; }
}