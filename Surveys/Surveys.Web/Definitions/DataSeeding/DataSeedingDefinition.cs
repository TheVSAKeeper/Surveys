using Surveys.Infrastructure.DatabaseInitialization;

namespace Surveys.Web.Definitions.DataSeeding;

/// <summary>
///     Seeding DbContext for default data for EntityFrameworkCore
/// </summary>
public class DataSeedingDefinition : AppDefinition
{
    /// <summary>
    ///     Configure application for current application
    /// </summary>
    /// <param name="app"></param>
    public override async void ConfigureApplication(WebApplication app)
    {
        const string DataPath = @"Definitions\DataSeeding\data\";

        await DatabaseInitializer.SeedUsers(app.Services, DataPath);
    }
}