namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class AnamnesisTemplateCreateViewModel
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public List<QuestionCreateViewModel> Questions { get; set; } = new();
    public int SortIndex { get; set; }
}