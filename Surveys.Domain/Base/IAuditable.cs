namespace Surveys.Domain.Base;

/// <summary>
///     Представляет информацию о создании и последнем обновлении
/// </summary>
public interface IAuditable
{
    /// <summary>
    ///     Время создания. Это значение никогда не изменяется
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Имя автора. Это значение никогда не изменяется
    /// </summary>
    string CreatedBy { get; set; }

    /// <summary>
    ///     Время последнего обновления значения. Должно быть обновлено, когда обновляются данные сущности
    /// </summary>
    DateTime? UpdatedAt { get; set; }

    /// <summary>
    ///     Автор последнего обновления значения. Должно быть обновлено, когда обновляются данные сущности
    /// </summary>
    string? UpdatedBy { get; set; }
}