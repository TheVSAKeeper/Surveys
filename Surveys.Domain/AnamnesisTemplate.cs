using Surveys.Domain.Base;

namespace Surveys.Domain;

public class AnamnesisTemplate : Identity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = null!;
    public virtual ICollection<Anamnesis>? Anamneses { get; set; }
}