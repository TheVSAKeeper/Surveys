using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation;

public class NavigationService<TViewModel>(INavigationMediator navigationMediator, CreateViewModel<TViewModel> createViewModel)
    : INavigationService where TViewModel : ViewModelBase
{
    public void Navigate()
    {
        navigationMediator.CurrentViewModel = createViewModel();
    }
}