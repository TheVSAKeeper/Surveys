using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

public class ResponseCreateViewModel
{
    public required Guid AnamnesisId { get; set; }
    public required Guid QuestionId { get; set; }
    public List<ResponseAnswerCreateViewModel>? Answers { get; set; }
}
