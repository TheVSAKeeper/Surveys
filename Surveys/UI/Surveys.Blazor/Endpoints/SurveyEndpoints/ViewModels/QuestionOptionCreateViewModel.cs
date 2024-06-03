namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class QuestionOptionCreateViewModel
{
    public Guid QuestionId { get; set; }
    public string Value { get; set; } = null!;
    public int SortIndex { get; set; }
}