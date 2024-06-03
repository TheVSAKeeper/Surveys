namespace Surveys.Blazor.Endpoints.AnamnesisTemplateEndpoints.ViewModels;

public class QuestionOptionViewModel
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public required string Value { get; set; }
    public int SortIndex { get; set; }
}