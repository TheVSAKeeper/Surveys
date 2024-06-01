using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

public class AnamnesisTemplateCreateViewModel
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public List<QuestionCreateViewModel> Questions { get; set; } = new();
    public int SortIndex { get; set; }
}