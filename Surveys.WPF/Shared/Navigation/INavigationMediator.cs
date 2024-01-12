using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation;

public interface INavigationMediator
{
    ViewModelBase CurrentViewModel { set; }
}