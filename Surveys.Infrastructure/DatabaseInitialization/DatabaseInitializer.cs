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

    public async Task SeedUsers()
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

        ApplicationUser developer = new()
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
}