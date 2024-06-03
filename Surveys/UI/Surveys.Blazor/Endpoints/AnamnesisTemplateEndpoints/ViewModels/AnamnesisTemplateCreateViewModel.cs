using Surveys.Blazor.Endpoints.QuestionEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.AnamnesisTemplateEndpoints.ViewModels;

public class AnamnesisTemplateCreateViewModel
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public List<QuestionCreateViewModel> Questions { get; set; } = new();
    public int SortIndex { get; set; }
}