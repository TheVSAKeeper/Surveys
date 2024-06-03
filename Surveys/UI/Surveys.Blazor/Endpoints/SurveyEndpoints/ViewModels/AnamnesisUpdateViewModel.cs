namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class AnamnesisUpdateViewModel
{
    public Guid Id { get; set; }
    public bool IsComplete { get; set; }
    public int SortIndex { get; set; }

    public List<ResponseUpdateViewModel>? Responses { get; set; }
}