namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class ResponseUpdateViewModel
{
    public required Guid Id { get; set; }
    public required List<ResponseAnswerUpdateViewModel> Answers { get; set; }
}