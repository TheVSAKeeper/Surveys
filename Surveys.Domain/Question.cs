using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Question : Identity
{
    public string Content { get; set; } = null!;

    public virtual ICollection<AnamnesisAnswer>? QuestionAnswers { get; set; }
}