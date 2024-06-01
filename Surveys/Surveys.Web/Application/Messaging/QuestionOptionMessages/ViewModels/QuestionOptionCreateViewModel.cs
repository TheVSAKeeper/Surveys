namespace Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

public class QuestionOptionCreateViewModel
{
    public Guid QuestionId { get; set; }
    public string Value { get; set; } = null!;
    public int SortIndex { get; set; }
}