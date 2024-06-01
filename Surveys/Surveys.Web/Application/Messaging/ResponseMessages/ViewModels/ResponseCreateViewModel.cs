using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

public class ResponseCreateViewModel
{
    public required Guid AnamnesisId { get; set; }
    public required List<ResponseAnswerCreateViewModel> Answers { get; set; }
}