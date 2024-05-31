using Surveys.Web.Application.Messaging.AnamnesisMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

public class SurveyUpdateViewModel
{
    public Guid Id { get; set; }

    public required string Complaint { get; set; }
    public required bool IsComplete { get; set; }

    public required List<AnamnesisViewModel>? Anamneses { get; set; }
}