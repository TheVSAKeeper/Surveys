namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

public class ResponseAnswerUpdateViewModel
{
    public Guid Id { get; set; }
    public required string Value { get; set; }
}