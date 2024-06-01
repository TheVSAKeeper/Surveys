using Surveys.Domain.Base;

namespace Surveys.Domain;

public class AnamnesisTemplate : SortableIdentity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required List<Question> Questions { get; set; }

    public virtual List<Anamnesis>? Anamneses { get; set; }
}