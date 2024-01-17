using Surveys.Domain.Base;

namespace Surveys.Domain;

public class AnamnesisTemplate : Identity
{
    public required string Name { get; set; }

    public required int SortIndex { get; set; }

    public virtual required IList<Question> Questions { get; set; }
    public virtual IList<Anamnesis>? Anamneses { get; set; }
}