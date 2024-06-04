using Surveys.Web.Application.Messaging.AnamnesisMessages;
using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

public class ResponseViewModel
{
    public Guid Id { get; set; }
    public Guid AnamnesisId { get; set; }
    public AnamnesisViewModel? Anamnesis { get; set; }
    public required Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }
    public required List<ResponseAnswerViewModel> Answers { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}