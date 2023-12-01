using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Surveys.Domain;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.DatabaseInitialization;

public static class DatabaseInitializer
{
    public static async void SeedUsers(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        await using ApplicationDbContext? context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        await context!.Database.EnsureCreatedAsync();
        IEnumerable<string> pending = await context.Database.GetPendingMigrationsAsync();

        if (pending.Any())
            await context!.Database.MigrateAsync();

        if (context.Users.Any())
            return;

        string[] roles = AppData.Roles.ToArray();

        foreach (string role in roles)
        {
            RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (!context!.Roles.Any(r => r.Name == role))
                await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
        }

        #region developer

        ApplicationUser developer1 = new()
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
                        PolicyName = "EventItems:UserRoles:View",
                        Description = "Access policy for EventItems controller user view"
                    }
                }
            }
        };

        if (!context!.Users.Any(u => u.UserName == developer1.UserName))
        {
            PasswordHasher<ApplicationUser> password = new();
            string hashed = password.HashPassword(developer1, "123qwe!@#");
            developer1.PasswordHash = hashed;
            ApplicationUserStore userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
            IdentityResult result = await userStore.CreateAsync(developer1);

            if (!result.Succeeded)
                throw new InvalidOperationException("Cannot create account");

            UserManager<ApplicationUser>? userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            foreach (string role in roles)
            {
                IdentityResult roleAdded = await userManager!.AddToRoleAsync(developer1, role);

                if (roleAdded.Succeeded)
                    await context.SaveChangesAsync();
            }
        }

        #endregion

        await context.SaveChangesAsync();
    }

    public static async void SeedEvents(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        await using ApplicationDbContext? context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        await context!.Database.EnsureCreatedAsync();

        IEnumerable<string> pending = await context.Database.GetPendingMigrationsAsync();

        if (pending.Any())
            await context!.Database.MigrateAsync();

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