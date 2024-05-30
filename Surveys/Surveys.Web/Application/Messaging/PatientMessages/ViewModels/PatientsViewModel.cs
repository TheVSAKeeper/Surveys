namespace Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

public class PatientViewModel
{
    public required Guid Id { get; set; }

    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? Patronymic { get; set; }

    public required Gender Gender { get; set; }

    public required DateOnly BirthDate { get; set; }
}