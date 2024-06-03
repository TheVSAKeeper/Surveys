namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.ViewModels;

public class ResponseCreateViewModel
{
    public required Guid AnamnesisId { get; set; }
    public required List<ResponseAnswerCreateViewModel> Answers { get; set; }
}