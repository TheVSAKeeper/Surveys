namespace Surveys.Blazor.Endpoints.QuestionEndpoints.ViewModels;

public class QuestionCreateViewModel
{
    public Guid AnamnesisTemplateId { get; set; }
    public string Text { get; set; } = null!;
    public QuestionType Type { get; set; }
    public List<QuestionOptionCreateViewModel>? Options { get; set; }
    public int SortIndex { get; set; }
}