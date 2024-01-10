using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.Domain;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.Navigation.Modal;

namespace Surveys.WPF.Features.Search.PatientSearch;

public class PatientSearchDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<PatientSearchFormViewModel>();

        services.AddModalNavigationService<PatientSearchFormViewModel>();
        
        services.AddCallbackNavigationService<Patient, PatientSearchFormViewModel>();

    }
}