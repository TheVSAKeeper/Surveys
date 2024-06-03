namespace Surveys.Blazor.Endpoints.AnamnesisTemplateEndpoints.ViewModels;

public class QuestionUpdateViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public QuestionType Type { get; set; }
    public List<QuestionOptionUpdateViewModel>? Options { get; set; }
    public int SortIndex { get; set; }
}