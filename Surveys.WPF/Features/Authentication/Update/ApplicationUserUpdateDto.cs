namespace Surveys.WPF.Features.Authentication.Update;

public class ApplicationUserUpdateDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? DisplayName { get; set; }
}