using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Anamnesis : Auditable
{
    public required Guid AnamnesisTemplateId { get; set; }
    public virtual AnamnesisTemplate? AnamnesisTemplate { get; set; }

    public virtual IList<AnamnesisAnswer>? AnamnesisAnswers { get; set; }

    public bool IsComplete { get; set; }

    public required int SortIndex { get; set; }

    // TODO: make Survey required
    public Guid? SurveyId { get; set; }
    public virtual Survey? Survey { get; set; }
}