using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Surveys.WPF.Definitions.Base;

public abstract class AppDefinition : IAppDefinition
{
    public virtual bool Enabled { get; protected set; } = true;

    public virtual void ConfigureServices(IServiceCollection builder, HostBuilderContext context)
    {
    }
}