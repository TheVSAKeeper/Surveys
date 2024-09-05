using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages;

public class AnamnesisViewModel
{
    public required Guid Id { get; set; }
    public required Guid SurveyId { get; set; }

    public required AnamnesisTemplateViewModel? AnamnesisTemplate { get; set; }

    public required bool IsComplete { get; set; }
    public int SortIndex { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public List<ResponseViewModel>? Responses { get; set; }
}
