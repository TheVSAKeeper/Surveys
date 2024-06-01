using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Survey : Auditable
{
    public required string Complaint { get; set; }

    public required Guid PatientId { get; set; }
    public virtual Patient? Patient { get; set; }

    public SurveyStatus Status { get; set; }

    public virtual List<Anamnesis>? Anamneses { get; set; }
    public virtual List<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
}