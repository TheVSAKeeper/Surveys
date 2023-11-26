using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Surveys.Infrastructure;

public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>
{
    public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer)
        : base(context, describer)
    {
    }

    public override Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        => Users
            .Include(x => x.ApplicationUserProfile)
            .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId, cancellationToken)!;

    public override Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        => Users
            .Include(x => x.ApplicationUserProfile)
            .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken)!;
}