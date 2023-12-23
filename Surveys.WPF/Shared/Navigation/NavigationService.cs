using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation;

public class NavigationService<TViewModel>(INavigationStore navigationStore, CreateViewModel<TViewModel> createViewModel)
    : INavigationService where TViewModel : ViewModelBase
{
    public void Navigate()
    {
        navigationStore.CurrentViewModel = createViewModel();
    }
}