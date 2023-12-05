using Calabonga.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.Infrastructure;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.UoW;

public class UnitOfWorkDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
        => services.AddUnitOfWork<ApplicationDbContext>();
}