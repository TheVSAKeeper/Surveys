using Surveys.Domain.Base;

namespace Surveys.Domain;

public class QuestionOption : SortableIdentity
{
    public required Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }

    public required string Value { get; set; }
}