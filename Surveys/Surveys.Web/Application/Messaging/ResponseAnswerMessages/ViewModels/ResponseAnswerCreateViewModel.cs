namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

public class ResponseAnswerCreateViewModel
{
    public required string Value { get; set; }
    public required Guid ResponseId { get; set; }
    public required Guid QuestionId { get; set; }
}