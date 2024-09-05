using Surveys.Domain;
using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

public class QuestionViewModel
{
    public Guid Id { get; set; }
    public Guid AnamnesisTemplateId { get; set; }
    public required string Text { get; set; }
    public QuestionType Type { get; set; }
    public List<QuestionOptionViewModel>? Options { get; set; }
    public List<ResponseViewModel>? Answers { get; set; }
    public int SortIndex { get; set; }
}
