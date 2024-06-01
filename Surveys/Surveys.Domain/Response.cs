using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Response : Auditable
{
    public required Guid AnamnesisId { get; set; }
    public virtual Anamnesis? Anamnesis { get; set; }

    public required List<ResponseAnswer> Answers { get; set; }
}