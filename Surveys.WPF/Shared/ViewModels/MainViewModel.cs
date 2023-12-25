using System.Windows.Input;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Shared.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly NavigationStore _navigationStore;

    public MainViewModel(
        ModalNavigationStore modalNavigationStore,
        NavigationStore navigationStore,
        NavigationService<HomeViewModel> homeNavigationService
    )
    {
        _modalNavigationStore = modalNavigationStore;
        _navigationStore = navigationStore;
        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
        _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModalChanged;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ICommand NavigateHomeCommand { get; }

    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public bool IsOpen => _modalNavigationStore.IsOpen;
    public bool IsEnabledBar => CurrentViewModel is not LoginViewModel;
    
    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
        OnPropertyChanged(nameof(IsEnabledBar));
    }

    private void OnCurrentModalViewModalChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsOpen));
    }

    public override void Dispose()
    {
        _modalNavigationStore.CurrentViewModelChanged -= OnCurrentModalViewModalChanged;
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;

        base.Dispose();
    }
}