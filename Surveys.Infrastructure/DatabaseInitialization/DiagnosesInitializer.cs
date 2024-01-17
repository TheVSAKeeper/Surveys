using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surveys.Domain;

namespace Surveys.Infrastructure.DatabaseInitialization;

public partial class DatabaseInitializer
{
    public async Task SeedDiagnoses()
    {
        _logger.LogDebug("[DatabaseInitializer] SeedDiagnoses start");

        ILogger<Diagnosis> logger = _scope.ServiceProvider.GetRequiredService<ILogger<Diagnosis>>();

        if (_context.Diagnoses.Any())
            return;

        string diagnosesPath = _dataPath + "diagnoses.txt";

        if (File.Exists(diagnosesPath) == false)
        {
            logger.LogError("[DatabaseInitializer] Not found {File}", diagnosesPath);
            return;
        }

        string[] lines = await File.ReadAllLinesAsync(diagnosesPath);

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
}