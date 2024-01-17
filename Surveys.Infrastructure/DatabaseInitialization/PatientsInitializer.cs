using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surveys.Domain;

namespace Surveys.Infrastructure.DatabaseInitialization;

public partial class DatabaseInitializer
{
    public async Task SeedPatients()
    {
        _logger.LogDebug("[DatabaseInitializer] SeedPatients start");

        ILogger<Patient> logger = _scope.ServiceProvider.GetRequiredService<ILogger<Patient>>();

        if (_context.Patients.Any())
            return;

        string path = _dataPath + "Patients.txt";

        if (File.Exists(path) == false)
        {
            logger.LogError("[DatabaseInitializer] Not found {File}", path);
            return;
        }

        string lines = await File.ReadAllTextAsync(path);

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