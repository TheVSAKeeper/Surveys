using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surveys.Domain;

namespace Surveys.Infrastructure.DatabaseInitialization;

public partial class DatabaseInitializer
{
    public async Task SeedAnamnesisTemplates()
    {
        _logger.LogDebug("[DatabaseInitializer] SeedAnamnesisTemplates start");

        ILogger<AnamnesisTemplate> logger = _scope.ServiceProvider.GetRequiredService<ILogger<AnamnesisTemplate>>();

        if (_context.AnamnesisTemplates.Any())
            return;

        string anamnesisTemplatesPath = _dataPath + "AnamnesisTemplates.txt";

        if (File.Exists(anamnesisTemplatesPath) == false)
        {
            logger.LogError("[DatabaseInitializer] Not found {File}", anamnesisTemplatesPath);
            return;
        }

        string lines = await File.ReadAllTextAsync(anamnesisTemplatesPath);

        AnamnesisTemplate[] anamnesisTemplates = lines.Split('/', StringSplitOptions.RemoveEmptyEntries)
            .Select((template, i) =>
            {
                string[] parts = template.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                List<Question> questions = parts
                    .Skip(1)
                    .Select((question, j) => new Question
                    {
                        Id = Guid.NewGuid(),
                        Content = question.Trim(['-', '?', ',', '.', ';']).Trim().ToLower() + "?",
                        SortIndex = j - 1
                    })
                    .ToList();

                return new AnamnesisTemplate
                {
                    Id = Guid.NewGuid(),
                    Name = parts[0].Trim(),
                    Questions = questions,
                    SortIndex = i
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
}