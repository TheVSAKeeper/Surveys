using Microsoft.Extensions.DependencyInjection;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages;

public static class AddPageExtensions
{
    public static IServiceCollection AddPage<TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : ViewModelBase
    {
        serviceCollection.AddTransient<TViewModel>();
        serviceCollection.AddNavigationService<TViewModel>();

        return serviceCollection;
    }
}