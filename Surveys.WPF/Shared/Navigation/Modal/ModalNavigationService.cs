using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation.Modal;

public class ModalNavigationService<TViewModel>(ModalNavigationMediator navigationMediator, CreateViewModel<TViewModel> createViewModel)
    : INavigationService where TViewModel : ViewModelBase
{
    public void Navigate()
    {
        navigationMediator.CurrentViewModel = createViewModel();
    }
}