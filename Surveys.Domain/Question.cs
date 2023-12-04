using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Question : Identity
{
    public string Content { get; set; } = null!;

    public AnamnesisTemplate AnamnesisTemplate { get; set; } = null!;

    public virtual ICollection<AnamnesisAnswer>? AnamnesisAnswers { get; set; }
}