namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class AnamnesisTemplateUpdateViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int SortIndex { get; set; }
}