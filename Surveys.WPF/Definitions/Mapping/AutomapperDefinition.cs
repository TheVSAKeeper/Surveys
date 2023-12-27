using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.Mapping;

public class AutomapperDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddAutoMapper(typeof(Program));
    }

    public override Task ConfigureApplication(IHost host)
    {
        IConfigurationProvider mapper = host.Services.GetRequiredService<IConfigurationProvider>();

        mapper.AssertConfigurationIsValid();
        return Task.CompletedTask;
        //mapper.CompileMappings();
    }
}