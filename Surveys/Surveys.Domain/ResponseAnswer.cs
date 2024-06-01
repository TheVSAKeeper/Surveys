using Surveys.Domain.Base;

namespace Surveys.Domain;

public class ResponseAnswer : Identity
{
    public required string Value { get; set; }

    public required Guid ResponseId { get; set; }
    public virtual Response? Response { get; set; }

    public required Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }
}