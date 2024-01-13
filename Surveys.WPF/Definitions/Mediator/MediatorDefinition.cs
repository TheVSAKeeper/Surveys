using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.Mediator;

public class MediatorDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {        
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());
    }
}