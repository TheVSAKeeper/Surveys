namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.ViewModels;

public class AnamnesisUpdateViewModel
{
    public Guid Id { get; set; }
    public bool IsComplete { get; set; }
    public int SortIndex { get; set; }

    public List<ResponseUpdateViewModel>? Responses { get; set; }
}