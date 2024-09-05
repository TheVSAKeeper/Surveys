using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Surveys.Infrastructure.DatabaseInitialization;

/// <summary>
///     Database Initializer
/// </summary>
public static class DatabaseInitializer
{
    /// <summary>
    ///     Seeds one default users to database for demo purposes only
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="dataPath"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task SeedUsers(IServiceProvider serviceProvider, string dataPath)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        await using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ILogger<ApplicationDbContext> logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        // ATTENTION!
        // -----------------------------------------------------------------------------
        // This is should not be used when UseInMemoryDatabase()
        // It should be uncomment when using UseSqlServer() settings or any other providers.
        // -----------------------------------------------------------------------------
        await context.Database.EnsureCreatedAsync();
        IEnumerable<string> pending = await context.Database.GetPendingMigrationsAsync();

        if (pending.Any())
        {
            await context.Database.MigrateAsync();
        }

        if (!context.Users.Any())
        {
            string[] roles = AppData.Roles.ToArray();

            foreach (string role in roles)
            {
                RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
                }
            }

            #region developer

            ApplicationUser developer1 = new()
            {
                Email = "microservice@survey.com",
                NormalizedEmail = "MICROSERVICE@SURVEY.COM",
                UserName = "microservice@survey.com",
                FirstName = "Microservice",
                LastName = "Administrator",
                NormalizedUserName = "MICROSERVICE@SURVEY.COM",
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

            if (!context.Users.Any(u => u.UserName == developer1.UserName))
            {
                PasswordHasher<ApplicationUser> password = new();
                string hashed = password.HashPassword(developer1, "123qwe!@#");
                developer1.PasswordHash = hashed;
                ApplicationUserStore userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
                IdentityResult result = await userStore.CreateAsync(developer1);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Cannot create account");
                }

                UserManager<ApplicationUser>? userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                foreach (string role in roles)
                {
                    IdentityResult roleAdded = await userManager!.AddToRoleAsync(developer1, role);

                    if (roleAdded.Succeeded)
                    {
                        await context.SaveChangesAsync();
                    }
                }
            }

            #endregion

            #region admin

            ApplicationUser admin = new()
            {
                Email = "admin@survey.com",
                NormalizedEmail = "ADMIN@SURVEY.COM",
                UserName = "admin@survey.com",
                FirstName = "Microservice",
                LastName = "Admin",
                NormalizedUserName = "Admin",
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

            if (!context.Users.Any(u => u.UserName == admin.UserName))
            {
                PasswordHasher<ApplicationUser> password = new();
                string hashed = password.HashPassword(admin, "123qwe!@#");
                admin.PasswordHash = hashed;
                ApplicationUserStore userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
                IdentityResult result = await userStore.CreateAsync(admin);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Cannot create account");
                }

                UserManager<ApplicationUser>? userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                IdentityResult roleAdded = await userManager!.AddToRoleAsync(admin, AppData.SystemAdministratorRoleName);

                if (roleAdded.Succeeded)
                {
                    await context.SaveChangesAsync();
                }
            }

            #endregion

            #region doctor

            ApplicationUser doctor = new()
            {
                Email = "doctor@survey.com",
                NormalizedEmail = "ADMIN@SURVEY.COM",
                UserName = "doctor@survey.com",
                FirstName = "Microservice",
                LastName = "Doctor",
                NormalizedUserName = "Doctor",
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

            if (!context.Users.Any(u => u.UserName == doctor.UserName))
            {
                PasswordHasher<ApplicationUser> password = new();
                string hashed = password.HashPassword(doctor, "123qwe!@#");
                doctor.PasswordHash = hashed;
                ApplicationUserStore userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
                IdentityResult result = await userStore.CreateAsync(doctor);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Cannot create account");
                }

                UserManager<ApplicationUser>? userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                IdentityResult roleAdded = await userManager!.AddToRoleAsync(doctor, AppData.DoctorRoleName);

                if (roleAdded.Succeeded)
                {
                    await context.SaveChangesAsync();
                }
            }

            #endregion

            #region nurse

            ApplicationUser nurse = new()
            {
                Email = "nurse@survey.com",
                NormalizedEmail = "ADMIN@SURVEY.COM",
                UserName = "nurse@survey.com",
                FirstName = "Microservice",
                LastName = "Nurse",
                NormalizedUserName = "Nurse",
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

            if (!context.Users.Any(u => u.UserName == nurse.UserName))
            {
                PasswordHasher<ApplicationUser> password = new();
                string hashed = password.HashPassword(nurse, "123qwe!@#");
                nurse.PasswordHash = hashed;
                ApplicationUserStore userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
                IdentityResult result = await userStore.CreateAsync(nurse);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Cannot create account");
                }

                UserManager<ApplicationUser>? userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                IdentityResult roleAdded = await userManager!.AddToRoleAsync(nurse, AppData.NurseRoleName);

                if (roleAdded.Succeeded)
                {
                    await context.SaveChangesAsync();
                }
            }

            #endregion

            await context.SaveChangesAsync();
        }

        if (!context.Patients.Any())
        {
            string path = dataPath + "patients.txt";

            if (File.Exists(path) == false)
            {
                logger.LogError("[SeedPatients] Not found {File}", path);
                return;
            }

            string lines = await File.ReadAllTextAsync(path);

            Patient[] patients = lines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(s =>
                {
                    string[] parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    DateOnly birthDate;

                    if (DateTime.TryParseExact(parts[4], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        birthDate = new DateOnly(parsedDate.Year, parsedDate.Month, parsedDate.Day);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Cannot parse data {parts[4]}");
                    }

                    return new Patient
                    {
                        LastName = parts[0],
                        FirstName = parts[1],
                        Patronymic = parts[2],
                        Gender = parts[3] switch
                        {
                            "М" => Gender.Male,
                            "Ж" => Gender.Female,
                            var _ => Gender.Unspecified
                        },
                        BirthDate = birthDate
                    };
                })
                .ToArray();

            await context.Patients.AddRangeAsync(patients);
            await context.SaveChangesAsync();
        }

        if (!context.AnamnesisTemplates.Any())
        {
            string path = dataPath + "AnamnesisTemplates.txt";

            if (File.Exists(path) == false)
            {
                logger.LogError("[SeedAnamnesisTemplates] Not found {File}", path);
                return;
            }

            string lines = await File.ReadAllTextAsync(path);

            List<AnamnesisTemplate> anamnesisTemplates = [];

            string[] templates = lines.Split('/', StringSplitOptions.RemoveEmptyEntries);

            foreach (string template in templates)
            {
                string[] parts = template.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                List<Question> questions = [];

                AnamnesisTemplate anamnesisTemplate = new()
                {
                    Id = Guid.NewGuid(),
                    Title = parts[0].Trim(),
                    Description = null,
                    SortIndex = anamnesisTemplates.Count + 1,
                    Questions = questions
                };

                for (int i = 1; i < parts.Length; i++)
                {
                    string part = parts[i];
                    string[] content = part.Split(["["], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    Question question = new()
                    {
                        Id = Guid.NewGuid(),
                        AnamnesisTemplateId = anamnesisTemplate.Id,
                        Text = content[0].Trim('-', '?', ',', '.', ';', '[', ']').Trim().ToLower() + "?",
                        Type = QuestionType.Text,
                        SortIndex = i
                    };

                    List<QuestionOption> options = [];

                    if (part.Contains('['))
                    {
                        options.AddRange(content[1]
                            .Split([","], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                            .Select((option, index) => new QuestionOption
                            {
                                Id = Guid.NewGuid(),
                                SortIndex = index,
                                QuestionId = question.Id,
                                Value = option.Trim('-', '?', ',', '.', ';', '[', ']').Trim().ToLower()
                            }));

                        question.Options = options;
                        question.Type = QuestionType.SingleChoice;
                    }

                    questions.Add(question);
                }

                anamnesisTemplates.Add(anamnesisTemplate);
            }

            context.AnamnesisTemplates.AddRange(anamnesisTemplates);
            await context.SaveChangesAsync();
        }
    }
}
