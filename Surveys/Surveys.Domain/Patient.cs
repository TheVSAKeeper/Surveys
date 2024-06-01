using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Patient : Identity
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? Patronymic { get; set; }

    public required Gender Gender { get; set; }

    public required DateOnly BirthDate { get; set; }

    public virtual List<Survey>? Surveys { get; set; }
    public virtual List<SurveyDiagnosis>? SurveyDiagnoses { get; set; }
}