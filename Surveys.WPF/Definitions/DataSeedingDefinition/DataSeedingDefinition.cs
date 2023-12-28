using Microsoft.Extensions.Hosting;
using Surveys.Infrastructure.DatabaseInitialization;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.DataSeedingDefinition;

public class DataSeedingDefinition : AppDefinition
{
    public override int OrderIndex => 1;

    public override async Task ConfigureApplication(IHost host)
    {
        DatabaseInitializer initializer = new(host.Services);
        
      await  initializer.SeedUsers();
      await   initializer.SeedDiagnoses();
    }
}