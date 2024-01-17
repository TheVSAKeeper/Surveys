using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Answer : Identity
{
    public required string Content { get; set; }

    public Guid AnamnesisAnswersId { get; set; }
    public virtual AnamnesisAnswer AnamnesisAnswers { get; set; } = null!;
}