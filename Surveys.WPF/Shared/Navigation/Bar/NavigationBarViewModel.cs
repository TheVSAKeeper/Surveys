using System.Windows.Input;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Shared.Navigation.Bar;

public class NavigationBarViewModel(NavigationService<HomeViewModel> homeNavigationService) : ViewModelBase
{
    public MainViewModel? MainViewModel { get; set; }

    public ICommand NavigateHomeCommand { get; } = new NavigateCommand(homeNavigationService);

    public bool IsEnabledHomeButton => MainViewModel?.CurrentViewModel is not LoginViewModel;
}