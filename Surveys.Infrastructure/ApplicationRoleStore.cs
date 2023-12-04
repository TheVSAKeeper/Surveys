using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Surveys.Infrastructure;

public class ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<ApplicationRole, ApplicationDbContext, Guid>(context, describer);