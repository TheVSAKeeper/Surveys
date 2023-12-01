using Surveys.Domain.Base;

namespace Surveys.Infrastructure;

/// <summary>
///     Представляет пользователя с информацией для входа (ApplicationUser)
/// </summary>
public class ApplicationUserProfile : Auditable
{
    /// <summary>
    ///     Учетная запись
    /// </summary>
    public virtual ApplicationUser? ApplicationUser { get; set; }

    /// <summary>
    ///     Разрешения приложения для авторизации на основе политик
    /// </summary>
    public List<AppPermission>? Permissions { get; set; }
}