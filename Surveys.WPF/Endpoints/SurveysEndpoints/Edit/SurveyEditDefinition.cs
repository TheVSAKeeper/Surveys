using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyEditDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<SurveyEditFormViewModel>();

        services.AddNavigationService<SurveyEditFormViewModel>();
    }
}