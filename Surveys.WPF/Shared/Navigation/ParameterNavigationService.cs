using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation;

public class ParameterNavigationService<TParameter, TViewModel>(INavigationStore navigationStore, Func<TParameter, TViewModel> createViewModel)
    where TViewModel : ViewModelBase
{
    public void Navigate(TParameter parameter)
    {
        navigationStore.CurrentViewModel = createViewModel(parameter);
    }
}