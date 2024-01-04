using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surveys.Domain;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.DatabaseInitialization;

public class DatabaseInitializer
{
    private const string DataPath = @"Definitions\DataSeedingDefinition\";

    private readonly ApplicationDbContext _context;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IServiceScope _scope;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
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

    public async Task SeedDiagnoses()
    {
        _logger.LogDebug("[DatabaseInitializer] SeedDiagnoses start");

        ILogger<Diagnosis> logger = _scope.ServiceProvider.GetRequiredService<ILogger<Diagnosis>>();

        if (_context.Diagnoses.Any())
            return;

        const string DiagnosesPath = DataPath + "diagnoses.txt";

        if (File.Exists(DiagnosesPath) == false)
        {
            logger.LogError("[DatabaseInitializer] Not found {File}", DiagnosesPath);
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

        logger.LogDebug("[DatabaseInitializer] Founded diagnosis: {FoundedDiagnosisCount}. Added: {AddedDiagnosisCount}",
            lines.Length,
            diagnosis.Length);

        await _context.Diagnoses.AddRangeAsync(diagnosis);
        await _context.SaveChangesAsync();

        _logger.LogDebug("[DatabaseInitializer] SeedDiagnoses end");
    }

    public async Task SeedAnamnesisTemplates()
    {
        _logger.LogDebug("[DatabaseInitializer] SeedAnamnesisTemplates start");

        ILogger<AnamnesisTemplate> logger = _scope.ServiceProvider.GetRequiredService<ILogger<AnamnesisTemplate>>();

        if (_context.AnamnesisTemplates.Any())
            return;

        const string AnamnesisTemplatesPath = DataPath + "AnamnesisTemplates.txt";

        if (File.Exists(AnamnesisTemplatesPath) == false)
        {
            logger.LogError("[DatabaseInitializer] Not found {File}", AnamnesisTemplatesPath);
            return;
        }

        string lines = await File.ReadAllTextAsync(AnamnesisTemplatesPath);

        AnamnesisTemplate[] anamnesisTemplates = lines.Split('/', StringSplitOptions.RemoveEmptyEntries)
            .Select(template =>
            {
                string[] parts = template.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                List<Question> questions = parts
                    .Skip(1)
                    .Select(question => new Question
                    {
                        Id = Guid.NewGuid(),
                        Content = question.Trim(['-']).Trim()
                    })
                    .ToList();

                return new AnamnesisTemplate
                {
                    Id = Guid.NewGuid(),
                    Name = parts[0].Trim(),
                    Questions = questions
                };
            })
            .ToArray();

        logger.LogDebug("[DatabaseInitializer] Founded AnamnesisTemplates: {FoundedAnamnesisTemplatesCount}. Added: {AddedAnamnesisTemplatesCount}",
            lines.Length,
            anamnesisTemplates.Length);

        await _context.AnamnesisTemplates.AddRangeAsync(anamnesisTemplates);
        await _context.SaveChangesAsync();

        _logger.LogDebug("[DatabaseInitializer] SeedAnamnesisTemplates end");
    }

    public async Task SeedPatients()
    {
        _logger.LogDebug("[DatabaseInitializer] SeedPatients start");

        ILogger<Patient> logger = _scope.ServiceProvider.GetRequiredService<ILogger<Patient>>();

        if (_context.Patients.Any())
            return;

        const string Path = DataPath + "Patients.txt";

        if (File.Exists(Path) == false)
        {
            logger.LogError("[DatabaseInitializer] Not found {File}", Path);
            return;
        }

        string lines = await File.ReadAllTextAsync(Path);

        Patient[] patients = lines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s =>
            {
                string[] parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

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
                    BirthDate = DateOnly.Parse(parts[4])
                };
            })
            .ToArray();

        logger.LogDebug("[DatabaseInitializer] Founded AnamnesisTemplates: {FoundedPatientsCount}. Added: {AddedPatientsCount}",
            lines.Length,
            patients.Length);

        await _context.Patients.AddRangeAsync(patients);
        await _context.SaveChangesAsync();

        _logger.LogDebug("[DatabaseInitializer] SeedPatients end");
    }
}