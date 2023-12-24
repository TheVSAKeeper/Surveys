using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surveys.Domain;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.DatabaseInitialization;

public static class DatabaseInitialization
{
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

        RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        foreach (string role in roles)
        {
            if (context.Roles.Any(applicationRole => applicationRole.Name == role))
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

        if (context.Users.Any(applicationUser => applicationUser.UserName == developer.UserName) == false)
        {
            PasswordHasher<ApplicationUser> password = new();

            string hashed = password.HashPassword(developer, "123qwe");

            developer.PasswordHash = hashed;

            ApplicationUserStore userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
            IdentityResult result = await userStore.CreateAsync(developer);

            if (result.Succeeded == false)
                throw new InvalidOperationException("Cannot create account");

            UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (string role in roles)
            {
                IdentityResult roleAdded = await userManager.AddToRoleAsync(developer, role);

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

    public static async void SeedDiagnoses(IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();

        ILogger<Diagnosis> logger = services.GetRequiredService<ILogger<Diagnosis>>();
        ApplicationDbContext context = await GetApplicationDbContext(scope);

        if (context.Diagnoses.Any())
            return;

        const string DiagnosesPath = "diagnoses.txt";

        if (File.Exists(DiagnosesPath) == false)
        {
            logger.LogError("[DatabaseInitialization] Not found {File}", DiagnosesPath);
            return;
        }

        string[] lines = await File.ReadAllLinesAsync(DiagnosesPath);

        Diagnosis[] diagnosis = lines
            .Select(line => line.Split('-', StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => new Diagnosis
            {
                Id = Guid.NewGuid(),
                Name = parts[0].Trim(),
                Description = parts[1].Trim()
            })
            .ToArray();

        logger.LogDebug("[DatabaseInitialization] Founded diagnosis: {FoundedDiagnosisCount}. Added: {AddedDiagnosisCount}",
            lines.Length,
            diagnosis.Length);

        await context.Diagnoses.AddRangeAsync(diagnosis);
        await context.SaveChangesAsync();
    }

    private static async Task<ApplicationDbContext> GetApplicationDbContext(IServiceScope scope)
    {
        ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>()
                                       ?? throw new InvalidOperationException($"{typeof(ApplicationDbContext)} dont registered");

        await context.Database.EnsureCreatedAsync();

        IEnumerable<string> pending = await context.Database.GetPendingMigrationsAsync();

        if (pending.Any())
            await context.Database.MigrateAsync();

        return context;
    }
}