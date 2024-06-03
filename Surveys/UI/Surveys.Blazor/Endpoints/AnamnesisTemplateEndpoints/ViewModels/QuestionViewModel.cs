using Surveys.Blazor.Endpoints.AnamnesisEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.AnamnesisTemplateEndpoints.ViewModels;

public class QuestionViewModel
{
    public Guid Id { get; set; }
    public Guid AnamnesisTemplateId { get; set; }
    public required string Text { get; set; }
    public QuestionType Type { get; set; }
    public List<QuestionOptionViewModel>? Options { get; set; }
    public List<ResponseAnswerViewModel>? Answers { get; set; }
    public int SortIndex { get; set; }
}

public enum QuestionType
{
    None,
    SingleChoice,
    MultipleChoice,
    Text,
    Number,
    Date
}