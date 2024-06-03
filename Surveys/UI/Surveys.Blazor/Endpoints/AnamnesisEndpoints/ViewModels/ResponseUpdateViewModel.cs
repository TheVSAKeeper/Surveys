namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.ViewModels;

public class ResponseUpdateViewModel
{
    public required Guid Id { get; set; }
    public required List<ResponseAnswerUpdateViewModel> Answers { get; set; }
}