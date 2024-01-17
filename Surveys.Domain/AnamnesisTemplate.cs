using Surveys.Domain.Base;

namespace Surveys.Domain;

public class AnamnesisTemplate : Identity
{
    public required string Name { get; set; }
    
    public required int SortIndex { get; set; }

    public required virtual ICollection<Question> Questions { get; set; }
    public virtual ICollection<Anamnesis>? Anamneses { get; set; }
}