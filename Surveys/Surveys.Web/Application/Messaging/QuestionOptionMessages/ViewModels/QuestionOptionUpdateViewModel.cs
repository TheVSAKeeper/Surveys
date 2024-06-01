namespace Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

public class QuestionOptionUpdateViewModel
{
    public Guid Id { get; set; }
    public string Value { get; set; } = null!;
    public int SortIndex { get; set; }
}