using System.Windows.Markup;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Shared.ViewModels;

[MarkupExtensionReturnType(typeof(MainViewModel))]
public class MainViewModel : ViewModelBase
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly NavigationStore _navigationStore;

    public MainViewModel(ModalNavigationStore modalNavigationStore, NavigationStore navigationStore)
    {
        _modalNavigationStore = modalNavigationStore;
        _navigationStore = navigationStore;

        _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModalChanged;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public bool IsOpen => _modalNavigationStore.IsOpen;

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
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