using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Create;

public class SurveyCreateDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<SurveyCreateFormViewModel>();

        services.AddNavigationService<SurveyCreateFormViewModel>();
    }
}