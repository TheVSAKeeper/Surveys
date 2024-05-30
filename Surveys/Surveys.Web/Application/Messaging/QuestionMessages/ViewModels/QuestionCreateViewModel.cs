namespace Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

public class QuestionCreateViewModel : IViewModel
{
    public required string Content { get; set; }

    public required int SortIndex { get; set; } = 0;

    public Guid? AnamnesisTemplateId { get; set; }
}