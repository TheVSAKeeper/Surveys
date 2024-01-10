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

        services.AddSingleton<ICallbackNavigationService<Patient>, CallbackModalNavigationService<Patient, PatientSearchFormViewModel>>(provider =>
            new CallbackModalNavigationService<Patient, PatientSearchFormViewModel>(provider.GetRequiredService<ModalNavigationStore>(), parameter =>
            {
                PatientSearchFormViewModel viewModel = provider.GetRequiredService<PatientSearchFormViewModel>();
                viewModel.Callback += parameter;
                return viewModel;
            }));
    }
}