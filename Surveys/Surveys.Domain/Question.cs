using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Question : SortableIdentity
{
    public required Guid AnamnesisTemplateId { get; set; }
    public AnamnesisTemplate? AnamnesisTemplate { get; set; }

    public required string Text { get; set; }
    public required QuestionType Type { get; set; }

    public virtual List<QuestionOption>? Options { get; set; }

    public virtual List<Response>? Answers { get; set; }
}
