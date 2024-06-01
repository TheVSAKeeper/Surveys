using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Anamnesis : SortableAuditable
{
    public required Guid SurveyId { get; set; }
    public virtual Survey? Survey { get; set; }

    public required Guid AnamnesisTemplateId { get; set; }
    public virtual AnamnesisTemplate? AnamnesisTemplate { get; set; }

    public bool IsComplete { get; set; }

    public virtual List<Response>? Responses { get; set; }
}