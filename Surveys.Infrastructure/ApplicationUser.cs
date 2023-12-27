using Microsoft.AspNetCore.Identity;

namespace Surveys.Infrastructure;

/// <summary>
///     Пользователь по умолчанию для приложения.
///     Добавьте данные профиля для пользователей приложения, добавив свойства в класс ApplicationUser
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? DisplayName { get; set; }

    public ICollection<ApplicationRole>? Roles { get; set; }
}