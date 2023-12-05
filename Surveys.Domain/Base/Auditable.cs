namespace Surveys.Domain.Base;

/// <summary>
///     Представляет 'аудиторию' таблицу из базы данных недвижимости
/// </summary>
public abstract class Auditable : Identity, IAuditable
{
    /// <summary>
    ///     Дата и время, когда создана сущность.
    ///     Никогда не меняется
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Имя пользователя, который создал сущность.
    ///     Никогда не меняется
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    ///     Последняя дата обновления сущности
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    ///     Автор последнего обновления
    /// </summary>
    public string? UpdatedBy { get; set; }
}