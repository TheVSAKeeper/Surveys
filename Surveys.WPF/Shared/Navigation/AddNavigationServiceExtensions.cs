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
            new NavigationService<TViewModel>(services.GetRequiredService<NavigationMediator>(), services.GetRequiredService<TViewModel>));
    }

    public static IServiceCollection AddModalNavigationService<TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : ViewModelBase
    {
        return serviceCollection.AddSingleton<ModalNavigationService<TViewModel>>(services =>
            new ModalNavigationService<TViewModel>(services.GetRequiredService<ModalNavigationMediator>(), services.GetRequiredService<TViewModel>));
    }

    public static IServiceCollection AddCallbackNavigationService<TParameter, TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : ViewModelBase, ICallbackViewModel<TParameter>
    {
        return serviceCollection.AddSingleton<ICallbackNavigationService<TParameter>, CallbackModalNavigationService<TParameter, TViewModel>>(provider =>
            new CallbackModalNavigationService<TParameter, TViewModel>(provider.GetRequiredService<ModalNavigationMediator>(), parameter =>
            {
                TViewModel viewModel = provider.GetRequiredService<TViewModel>();
                viewModel.SetCallback(parameter);
                return viewModel;
            }));
    }

    public static IServiceCollection AddParameterNavigationService<TParameter, TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : ViewModelBase, IParameterViewModel<TParameter>
    {
        return serviceCollection.AddSingleton<ParameterNavigationService<TParameter, TViewModel>>(provider =>
            new ParameterNavigationService<TParameter, TViewModel>(provider.GetRequiredService<ModalNavigationMediator>(), parameter =>
            {
                TViewModel viewModel = provider.GetRequiredService<TViewModel>();
                viewModel.SetParameter(parameter);
                return viewModel;
            }));
    }
}