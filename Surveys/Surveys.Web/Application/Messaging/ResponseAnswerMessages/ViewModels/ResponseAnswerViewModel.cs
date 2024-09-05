namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

public class ResponseAnswerViewModel
{
    public Guid Id { get; set; }
    public required string Value { get; set; }
    public Guid ResponseId { get; set; }
}
