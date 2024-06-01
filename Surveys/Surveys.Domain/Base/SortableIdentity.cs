namespace Surveys.Domain.Base;

/// <summary>
///     Order
/// </summary>
public class SortableIdentity : Identity, ISortable
{
    /// <summary>
    ///     Sorting index for entity
    /// </summary>
    public int SortIndex { get; set; }
}