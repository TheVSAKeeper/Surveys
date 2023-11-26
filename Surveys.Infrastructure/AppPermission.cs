using Surveys.Domain.Base;

namespace Surveys.Infrastructure;

public class AppPermission : Auditable
{
    public Guid ApplicationUserProfileId { get; set; }

    public virtual ApplicationUserProfile? ApplicationUserProfile { get; set; }

    /// <summary>
    ///     Authorize attribute policy name
    /// </summary>
    public string PolicyName { get; set; } = null!;

    public string Description { get; set; } = null!;
}