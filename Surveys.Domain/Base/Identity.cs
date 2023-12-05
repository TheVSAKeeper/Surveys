namespace Surveys.Domain.Base;

/// <summary>
///     Идентификатор
/// </summary>
public abstract class Identity : IHaveId
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}