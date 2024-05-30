namespace Surveys.Web.Application.Messaging.AnamnesisMessages.ViewModels;

public class AnamnesisViewModel
{
    public Guid Id { get; set; }

    public Guid AnamnesisTemplateId { get; set; }

    public bool IsComplete { get; set; }
    public int SortIndex { get; set; }

    public Guid? SurveyId { get; set; }

    public List<AnamnesisAnswer>? AnamnesisAnswers { get; set; }
}