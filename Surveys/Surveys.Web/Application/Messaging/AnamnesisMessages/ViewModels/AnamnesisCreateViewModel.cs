namespace Surveys.Web.Application.Messaging.AnamnesisMessages;

public class AnamnesisCreateViewModel
{
    public Guid Id { get; set; }

    public required Guid SurveyId { get; set; }
    public required Guid AnamnesisTemplateId { get; set; }
    public required int SortIndex { get; set; }
}