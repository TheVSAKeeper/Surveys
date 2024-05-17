using Surveys.Domain;
using Surveys.Domain.Base;

namespace Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

public class PatientCreateViewModel : IViewModel
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? Patronymic { get; set; }

    public required Gender Gender { get; set; }

    public required DateOnly BirthDate { get; set; }
}