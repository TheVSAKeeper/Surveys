namespace Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

public class QuestionViewModel
{
    public required string Content { get; set; }

    public required int SortIndex { get; set; }

    public Guid? AnamnesisTemplateId { get; set; }
}