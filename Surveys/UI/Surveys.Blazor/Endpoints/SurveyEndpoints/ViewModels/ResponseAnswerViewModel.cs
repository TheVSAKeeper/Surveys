namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class ResponseAnswerViewModel
{
    public Guid Id { get; set; }
    public required string Value { get; set; }
    public Guid ResponseId { get; set; }
    public Guid QuestionId { get; set; }
}