namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

public class ResponseAnswerCreateViewModel
{
    public required string Value { get; set; }
    public Guid ResponseId { get; set; }
    public Guid QuestionId { get; set; }
}