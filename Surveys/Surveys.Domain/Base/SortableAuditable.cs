namespace Surveys.Domain.Base;

public class SortableAuditable : Auditable, ISortable
{
    /// <summary>
    ///     Sorting index for entity
    /// </summary>
    public int SortIndex { get; set; }
}
