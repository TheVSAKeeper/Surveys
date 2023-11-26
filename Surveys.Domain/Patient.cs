using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Patient : Identity
{
    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateOnly BirthDate { get; set; }

    public virtual ICollection<Survey>? Surveys { get; set; }
    public virtual ICollection<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
}