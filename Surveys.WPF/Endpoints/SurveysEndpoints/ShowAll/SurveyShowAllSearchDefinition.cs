using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

public class SurveyShowAllSearchDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<SurveyShowAllFormViewModel>();

        services.AddNavigationService<SurveyShowAllFormViewModel>();
        services.AddModalNavigationService<SurveyShowAllFormViewModel>();
    }
}