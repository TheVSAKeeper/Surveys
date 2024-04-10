using Microsoft.Extensions.Hosting;
using Surveys.Infrastructure.DatabaseInitialization;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.DataSeeding;

public class DataSeedingDefinition : AppDefinition
{
    public override int OrderIndex => 1;

    public override Task ConfigureApplication(IHost host)
    {
        const string DataPath = @"Definitions\DataSeeding\data\";

        DatabaseInitializer initializer = new(host.Services, DataPath);

        initializer.SeedUsers();
        initializer.SeedDiagnoses();
        initializer.SeedAnamnesisTemplates();
        initializer.SeedPatients();
        return Task.CompletedTask;
    }
}