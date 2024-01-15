using Surveys.Domain.Base;

namespace Surveys.Domain;

public class AnamnesisAnswer : Identity
{
    public Guid QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;

    public virtual IList<Answer>? Answers { get; set; }

    public Guid AnamnesisId { get; set; }
    public virtual Anamnesis? Anamnesis { get; set; }
}