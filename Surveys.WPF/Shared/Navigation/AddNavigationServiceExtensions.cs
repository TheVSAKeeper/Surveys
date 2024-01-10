using Microsoft.Extensions.DependencyInjection;
using Surveys.WPF.Shared.Navigation.Modal;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation;

public static class AddNavigationServiceExtensions
{
    public static IServiceCollection AddNavigationService<TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : ViewModelBase
    {
        return serviceCollection.AddSingleton<NavigationService<TViewModel>>(services =>
            new NavigationService<TViewModel>(services.GetRequiredService<NavigationStore>(), services.GetRequiredService<TViewModel>));
    }

    public static IServiceCollection AddModalNavigationService<TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : ViewModelBase
    {
        return serviceCollection.AddSingleton<ModalNavigationService<TViewModel>>(services =>
            new ModalNavigationService<TViewModel>(services.GetRequiredService<ModalNavigationStore>(), services.GetRequiredService<TViewModel>));
    }

    public static IServiceCollection AddCallbackNavigationService<TParameter, TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : ViewModelBase, ICallbackViewModel<TParameter>
    {
        return serviceCollection.AddSingleton<ICallbackNavigationService<TParameter>, CallbackModalNavigationService<TParameter, TViewModel>>(provider =>
            new CallbackModalNavigationService<TParameter, TViewModel>(provider.GetRequiredService<ModalNavigationStore>(), parameter =>
            {
                TViewModel viewModel = provider.GetRequiredService<TViewModel>();
                viewModel.SetCallback(parameter);
                return viewModel;
            }));
    }
}