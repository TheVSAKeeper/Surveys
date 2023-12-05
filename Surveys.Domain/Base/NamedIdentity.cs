namespace Surveys.Domain.Base;

/// <summary>
///     Словарь NamedIdentity для селектора
/// </summary>
public abstract class NamedIdentity : Identity
{
    /// <summary>
    ///     Имя сущности
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Краткое имя сущности
    /// </summary>
    public string? BriefName { get; set; }

    /// <summary>
    ///     Краткое описание
    /// </summary>
    public string? Description { get; set; }
}