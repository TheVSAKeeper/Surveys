namespace Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

public class ResponseAnswerUpdateViewModel
{
    public Guid Id { get; set; }
    public required string Value { get; set; }
}