using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Pages.Profile;
using Surveys.WPF.Pages.Survey;

namespace Surveys.WPF.Pages;

public class PagesDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddPage<HomeViewModel>();
        services.AddPage<LoginViewModel>();
        services.AddPage<ProfileViewModel>();
        services.AddPage<SurveyViewModel>();
    }
}