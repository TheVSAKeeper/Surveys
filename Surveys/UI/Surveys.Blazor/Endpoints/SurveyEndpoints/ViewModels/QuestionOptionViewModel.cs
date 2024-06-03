namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class QuestionOptionViewModel
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public required string Value { get; set; }
    public int SortIndex { get; set; }
}