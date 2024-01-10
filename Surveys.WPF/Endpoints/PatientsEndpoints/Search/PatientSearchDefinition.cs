using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.Domain;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.PatientsEndpoints.Search;

public class PatientSearchDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<PatientSearchFormViewModel>();

        services.AddModalNavigationService<PatientSearchFormViewModel>();

        services.AddCallbackNavigationService<Patient, PatientSearchFormViewModel>();
    }
}