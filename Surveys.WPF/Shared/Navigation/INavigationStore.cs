using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation;

public interface INavigationStore
{
    ViewModelBase CurrentViewModel { set; }
}