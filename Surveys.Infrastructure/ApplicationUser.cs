using Microsoft.AspNetCore.Identity;

namespace Surveys.Infrastructure;

/// <summary>
///     Default user for application.
///     Add profile data for application users by adding properties to the ApplicationUser class
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
  
    public string? LastName { get; set; }

    public Guid? ApplicationUserProfileId { get; set; }

    public virtual ApplicationUserProfile? ApplicationUserProfile { get; set; }
}