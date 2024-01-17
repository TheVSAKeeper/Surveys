using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Question : Identity
{
    public required string Content { get; set; }

    public required int SortIndex { get; set; }

    public AnamnesisTemplate? AnamnesisTemplate { get; set; }

    public virtual ICollection<AnamnesisAnswer>? AnamnesisAnswers { get; set; }
}