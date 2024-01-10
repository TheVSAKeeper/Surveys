using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.Domain;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.AnamnesesEndpoints.Create;

public class AnamnesesCreateDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<AnamnesesCreateFormViewModel>();

        services.AddModalNavigationService<AnamnesesCreateFormViewModel>();

        services.AddCallbackNavigationService<List<Anamnesis>, AnamnesesCreateFormViewModel>();
    }
}