using Surveys.Blazor.Endpoints.QuestionEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.AnamnesisTemplateEndpoints.ViewModels;

public class AnamnesisTemplateViewModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required List<QuestionViewModel> Questions { get; set; }
    public bool IsSelected { get; set; }
    public int SortIndex { get; set; }
}