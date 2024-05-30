namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

public class AnamnesisTemplateViewModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    public List<Question>? Questions { get; set; }
    public bool IsSelected { get; set; }
    public int SortIndex { get; set; }
}