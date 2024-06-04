namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.ViewModels;

public class ResponseViewModel
{
    public Guid Id { get; set; }
    public Guid AnamnesisId { get; set; }
    public AnamnesisViewModel? Anamnesis { get; set; }
    public List<ResponseAnswerViewModel> Answers { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}