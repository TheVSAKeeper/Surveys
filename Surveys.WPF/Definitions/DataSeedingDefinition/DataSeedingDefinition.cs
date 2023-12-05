using Microsoft.Extensions.Hosting;
using Surveys.Infrastructure.DatabaseInitialization;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.DataSeedingDefinition;

public class DataSeedingDefinition : AppDefinition
{
    public override void ConfigureApplication(IHost host)
    {
        //DatabaseInitialization.SeedUsers(host.Services);
        DatabaseInitialization.SeedEvents(host.Services);
        DatabaseInitialization.SeedDiagnoses(host.Services);
    }
}