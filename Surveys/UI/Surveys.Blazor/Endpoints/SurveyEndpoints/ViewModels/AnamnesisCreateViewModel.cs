namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class AnamnesisCreateViewModel
{
    public Guid Id { get; set; }

    public Guid SurveyId { get; set; }
    public Guid AnamnesisTemplateId { get; set; }
    public int SortIndex { get; set; }
}