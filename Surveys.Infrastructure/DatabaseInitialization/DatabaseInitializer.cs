using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surveys.Domain;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.DatabaseInitialization;

public partial class DatabaseInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IServiceScope _scope;
    private readonly string _dataPath;

    public DatabaseInitializer(IServiceProvider serviceProvider, string dataPath)
    {
        _dataPath = dataPath;
        _scope = serviceProvider.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _logger = _scope.ServiceProvider.GetRequiredService<ILogger<DatabaseInitializer>>();
    }

    public async void SeedUsers()
    {
        _logger.LogDebug("[DatabaseInitializer] SeedUsers start");

        if (_context.Users.Any())
            return;

        string[] roles = AppData.Roles.ToArray();

        RoleManager<ApplicationRole> roleManager = _scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        foreach (string role in roles)
        {
            if (_context.Roles.Any(applicationRole => applicationRole.Name == role))
                continue;

            ApplicationRole applicationRole = new()
            {
                Name = role,
                NormalizedName = role.ToUpper()
            };

            await roleManager.CreateAsync(applicationRole);
        }

        ApplicationRole administratorRole = (await roleManager.FindByNameAsync(AppData.SystemAdministratorRoleName))!;

        #region developer

        /*ApplicationUser developer = new()
        {
            Id = Guid.Parse("35a9b0d1-1206-4b9f-9e9e-0dbaf280d3e8"),
            UserName = "Superuser",
            DisplayName = "Superuser",
            FirstName = "Survey",
            LastName = "Administrator",
            Patronymic = "Patronymic",
            NormalizedUserName = "SUPERUSER",
            PhoneNumber = "+79000000000",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            Roles = new List<ApplicationRole>
            {
                administratorRole
            }
        };*/

        ApplicationUser developer = new()
        {
            Email = "microservice@yopmail.com",
            NormalizedEmail = "MICROSERVICE@YOPMAIL.COM",
            UserName = "microservice@yopmail.com",
            FirstName = "Microservice",
            LastName = "Administrator",
            NormalizedUserName = "MICROSERVICE@YOPMAIL.COM",
            PhoneNumber = "+79000000000",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ApplicationUserProfile = new ApplicationUserProfile
            {
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "SEED",
                Permissions = new List<AppPermission>
                {
                    new()
                    {
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "SEED",
                        PolicyName = "Profiles:Roles:Get",
                        Description = "Access policy for view Roles in user Profiles"
                    }
                }
            }
        };

        if (_context.Users.Any(applicationUser => applicationUser.UserName == developer.UserName) == false)
        {
            PasswordHasher<ApplicationUser> password = new();

            string hashed = password.HashPassword(developer, "123qwe");

            developer.PasswordHash = hashed;

            ApplicationUserStore userStore = _scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
            IdentityResult result = await userStore.CreateAsync(developer);

            if (result.Succeeded == false)
                throw new InvalidOperationException("Cannot create account");

            UserManager<ApplicationUser> userManager = _scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (string role in roles)
            {
                IdentityResult roleAdded = await userManager.AddToRoleAsync(developer, role);

                if (roleAdded.Succeeded)
                    await _context.SaveChangesAsync();
            }
        }

        #endregion

        await _context.SaveChangesAsync();

        _logger.LogDebug("[DatabaseInitializer] SeedUsers end");
    }
        public static async void SeedUsers(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // ATTENTION!
        // -----------------------------------------------------------------------------
        // This is should not be used when UseInMemoryDatabase()
        // It should be uncomment when using UseSqlServer() settings or any other providers.
        // -----------------------------------------------------------------------------
        //await context!.Database.EnsureCreatedAsync();
        //var pending = await context.Database.GetPendingMigrationsAsync();
        //if (pending.Any())
        //{
        //    await context!.Database.MigrateAsync();
        //}

        if (context.Users.Any())
        {
            return;
        }

        var roles = AppData.Roles.ToArray();

        foreach (var role in roles)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (!context!.Roles.Any(r => r.Name == role))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
            }
        }

        #region developer

        var developer1 = new ApplicationUser
        {
            Email = "microservice@yopmail.com",
            NormalizedEmail = "MICROSERVICE@YOPMAIL.COM",
            UserName = "microservice@yopmail.com",
            FirstName = "Microservice",
            LastName = "Administrator",
            NormalizedUserName = "MICROSERVICE@YOPMAIL.COM",
            PhoneNumber = "+79000000000",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ApplicationUserProfile = new ApplicationUserProfile
            {
                CreatedAt = DateTime.Now,
                CreatedBy = "SEED",
                Permissions = new List<AppPermission>
                {
                    new()
                    {
                        CreatedAt = DateTime.Now,
                        CreatedBy = "SEED",
                        PolicyName = "Profiles:Roles:Get",
                        Description = "Access policy for view Roles in user Profiles"
                    }
                }
            }
        };

        if (!context!.Users.Any(u => u.UserName == developer1.UserName))
        {
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(developer1, "123qwe!@#");
            developer1.PasswordHash = hashed;
            var userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
            var result = await userStore.CreateAsync(developer1);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Cannot create account");
            }

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            foreach (var role in roles)
            {
                var roleAdded = await userManager!.AddToRoleAsync(developer1, role);

                if (roleAdded.Succeeded)
                {
                    await context.SaveChangesAsync();
                }
            }
        }

        #endregion

        await context.SaveChangesAsync();
    }
    /// <summary>
    /// Seeds one event to database for demo purposes only
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static async void SeedEvents(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // ATTENTION!
        // -----------------------------------------------------------------------------
        // This is should not be used when UseInMemoryDatabase()
        // It should be uncomment when using UseSqlServer() settings or any other providers.
        // -----------------------------------------------------------------------------
        //await context!.Database.EnsureCreatedAsync();
        //var pending = await context.Database.GetPendingMigrationsAsync();
        //if (pending.Any())
        //{
        //    await context!.Database.MigrateAsync();
        //}

        if (context.EventItems.Any())
        {
            return;
        }

        await context.EventItems.AddAsync(new EventItem
        {
            CreatedAt = DateTime.UtcNow,
            Id = Guid.Parse("1467a5b9-e61f-82b0-425b-7ec75f5c5029"),
            Level = "Information",
            Logger = "SEED",
            Message = "Seed method some entities successfully save to ApplicationDbContext"
        });

        await context.SaveChangesAsync();
    }
}