using Surveys.Web.Application.Messaging.AnamnesisMessages;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

public class SurveyUpdateViewModel
{
    public Guid Id { get; set; }

    public required string Complaint { get; set; }
    public required PatientViewModel Patient { get; set; }
    public required SurveyStatus Status { get; set; }

    public required List<AnamnesisViewModel> Anamneses { get; set; }
}