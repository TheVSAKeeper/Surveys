namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class ResponseCreateViewModel
{
    public required Guid AnamnesisId { get; set; }
    public required List<ResponseAnswerCreateViewModel> Answers { get; set; }
}