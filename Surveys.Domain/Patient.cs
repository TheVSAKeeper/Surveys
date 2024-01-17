using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Patient : Identity
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? Patronymic { get; set; }

    public required Gender Gender { get; set; }

    public required DateOnly BirthDate { get; set; }

    public virtual IList<Survey>? Surveys { get; set; }
    public virtual IList<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Unspecified = 3
}