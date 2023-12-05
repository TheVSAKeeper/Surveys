namespace Surveys.Domain.Base;

/// <summary>
///     Общий интерфейс идентификатора
/// </summary>
public interface IHaveId
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    Guid Id { get; set; }
}