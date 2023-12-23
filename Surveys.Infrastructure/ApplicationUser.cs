using Microsoft.AspNetCore.Identity;

namespace Surveys.Infrastructure;

/// <summary>
///     Пользователь по умолчанию для приложения.
///     Добавьте данные профиля для пользователей приложения, добавив свойства в класс ApplicationUser
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    public string? Patronymic { get; set; }

    public Guid? ApplicationUserProfileId { get; set; }

    public virtual ApplicationUserProfile? ApplicationUserProfile { get; set; }
}