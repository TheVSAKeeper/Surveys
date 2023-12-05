using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Surveys.Domain;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.DatabaseInitialization;

/// <summary>
///     Инициализатор базы данных
/// </summary>
public static class DatabaseInitialization
{
    /// <summary>
    ///     Заполняет базу данных одними начальными данными пользователя для демонстрационных целей
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static async void SeedUsers(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        await using ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>()
                                                   ?? throw new InvalidOperationException($"{typeof(ApplicationDbContext)} dont registered");

        await context.Database.EnsureCreatedAsync();
        
        IEnumerable<string> pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
            await context.Database.MigrateAsync();

        if (context.Users.Any())
            return;

        string[] roles = AppData.Roles.ToArray();

        foreach (string role in roles)
        {
            RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (context.Roles.Any(applicationRole => applicationRole.Name == role))
                continue;

            ApplicationRole applicationRole = new()
            {
                Name = role,
                NormalizedName = role.ToUpper()
            };

            await roleManager.CreateAsync(applicationRole);
        }

        #region developer

        ApplicationUser developer = new()
        {
            UserName = "Superuser",
            FirstName = "Survey",
            LastName = "Administrator",
            NormalizedUserName = "SUPERUSER",
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
                        PolicyName = "EventItems:UserRoles:View",
                        Description = "Access policy for EventItems controller user view"
                    }
                }
            }
        };

        if (context.Users.Any(applicationUser => applicationUser.UserName == developer.UserName) == false)
        {
            PasswordHasher<ApplicationUser> password = new();

            string hashed = password.HashPassword(developer, "123qwe");
            developer.PasswordHash = hashed;

            ApplicationUserStore userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
            IdentityResult result = await userStore.CreateAsync(developer);

            if (result.Succeeded == false)
                throw new InvalidOperationException("Cannot create account");

            UserManager<ApplicationUser>? userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            foreach (string role in roles)
            {
                IdentityResult roleAdded = await userManager!.AddToRoleAsync(developer, role);

                if (roleAdded.Succeeded)
                    await context.SaveChangesAsync();
            }
        }

        #endregion

        await context.SaveChangesAsync();
    }

    /// <summary>
    ///     Заполняет базу данных одним событием для демонстрационных целей
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static async void SeedEvents(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        await using ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>()
                                                   ?? throw new InvalidOperationException($"{typeof(ApplicationDbContext)} dont registered");

        await context.Database.EnsureCreatedAsync();

        IEnumerable<string> pending = await context.Database.GetPendingMigrationsAsync();

        if (pending.Any())
            await context.Database.MigrateAsync();

        if (context.EventItems.Any())
            return;

        await context.EventItems.AddAsync(new EventItem
        {
            CreatedAt = DateTime.UtcNow,
            Id = Guid.Parse("1467a5b9-e61f-82b0-425b-7ec75f5c5029"),
            Level = "Information",
            Logger = "SEED",
            Message = "Seed method some entities successfully save to ApplicationDbContext"
        });
    }
}