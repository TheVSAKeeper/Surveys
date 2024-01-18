using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Question : Identity
{
    public required ContentType Type { get; set; }
    public required string Content { get; set; }
    public required int SortIndex { get; set; }
    
    public AnamnesisTemplate? AnamnesisTemplate { get; set; }

    public virtual IList<AnamnesisAnswer>? AnamnesisAnswers { get; set; }
}