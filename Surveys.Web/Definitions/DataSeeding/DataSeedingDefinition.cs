using Calabonga.AspNetCore.AppDefinitions;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure;

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
    public override void ConfigureApplication(WebApplication app)
    {
        // TODO
        
        // DatabaseInitializer.SeedUsers(app.Services);
        // DatabaseInitializer.SeedEvents(app.Services);
    }
}