namespace Surveys.Web.Application.Messaging.AnamnesisMessages.ViewModels;

public class AnamnesisCreateViewModel
{
    public Guid Id { get; set; }

    public required Guid AnamnesisTemplateId { get; set; }

    public required int SortIndex { get; set; }

    public Guid? SurveyId { get; set; }
}