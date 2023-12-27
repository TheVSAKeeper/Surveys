using Microsoft.Extensions.Hosting;
using Surveys.Infrastructure.DatabaseInitialization;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.DataSeedingDefinition;

public class DataSeedingDefinition : AppDefinition
{
    public override int OrderIndex => 1;

    public override Task ConfigureApplication(IHost host)
    {
        DatabaseInitialization.SeedUsers(host.Services);
        DatabaseInitialization.SeedDiagnoses(host.Services);

        return Task.CompletedTask;
    }
}