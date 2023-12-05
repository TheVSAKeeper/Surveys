using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Surveys.WPF.Definitions.Base;

public interface IAppDefinition
{
    bool Enabled { get; }
    void ConfigureServices(IServiceCollection builder, HostBuilderContext context);
}