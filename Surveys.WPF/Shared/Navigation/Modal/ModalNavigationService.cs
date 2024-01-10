using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation.Modal;

public class ModalNavigationService<TViewModel>(ModalNavigationStore navigationStore, CreateViewModel<TViewModel> createViewModel)
    : INavigationService where TViewModel : ViewModelBase
{
    public void Navigate()
    {
        navigationStore.CurrentViewModel = createViewModel();
    }
}