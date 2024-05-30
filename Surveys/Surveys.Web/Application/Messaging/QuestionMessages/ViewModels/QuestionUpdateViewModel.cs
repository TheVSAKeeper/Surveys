namespace Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

public class QuestionUpdateViewModel : ViewModelBase
{
    public string? Content { get; set; }

    public int? SortIndex { get; set; }

    public AnamnesisTemplate? AnamnesisTemplate { get; set; }
}