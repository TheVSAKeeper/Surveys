namespace Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

public class SurveyCreateViewModel
{
    public Guid Id { get; set; }

    public required string Complaint { get; set; }

    public required Guid PatientId { get; set; }
}