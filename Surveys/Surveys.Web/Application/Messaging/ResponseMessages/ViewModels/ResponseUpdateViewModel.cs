using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

public class ResponseUpdateViewModel
{
    public required Guid Id { get; set; }
    public required List<ResponseAnswerUpdateViewModel> Answers { get; set; }
}