namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.ViewModels;

public class ResponseAnswerCreateViewModel
{
    public required string Value { get; set; }
    public Guid ResponseId { get; set; }
    public Guid QuestionId { get; set; }
}