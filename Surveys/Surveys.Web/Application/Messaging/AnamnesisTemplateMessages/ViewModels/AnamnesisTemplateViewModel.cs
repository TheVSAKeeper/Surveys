using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

public class AnamnesisTemplateViewModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required List<QuestionViewModel> Questions { get; set; }
    public bool IsSelected { get; set; }
    public int SortIndex { get; set; }
}
