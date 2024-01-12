using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation;

public class ParameterNavigationService<TParameter, TViewModel>(INavigationMediator navigationMediator, Func<TParameter, TViewModel> createViewModel)
    where TViewModel : ViewModelBase
{
    public void Navigate(TParameter parameter)
    {
        navigationMediator.CurrentViewModel = createViewModel(parameter);
    }
}