using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.Infrastructure;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.DbContext;

public class DbContextDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(context.Configuration.GetConnectionString(nameof(ApplicationDbContext)));
        });

        services.AddIdentityCore<ApplicationUser>()
          //  .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddUserStore<ApplicationUserStore>()
           // .AddRoleStore<ApplicationRoleStore>()
           // .AddUserManager<UserManager<ApplicationUser>>()
          ;
    }
}