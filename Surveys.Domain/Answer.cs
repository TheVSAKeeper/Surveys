using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Answer : Identity
{
    public string Content { get; set; } = null!;

    public Guid QuestionAnswersId { get; set; }
    public virtual AnamnesisAnswer QuestionAnswers { get; set; } = null!;
}