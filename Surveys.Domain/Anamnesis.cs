using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Anamnesis : Identity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<AnamnesisAnswer>? Answers { get; set; }

    public Guid SurveyId { get; set; }
    public virtual Survey Survey { get; set; } = null!;
}