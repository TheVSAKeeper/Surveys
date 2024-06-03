namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class AnamnesisTemplateViewModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public List<QuestionViewModel> Questions { get; set; } = new();
    public int SortIndex { get; set; }
}