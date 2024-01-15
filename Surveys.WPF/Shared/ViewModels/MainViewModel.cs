using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.Navigation.Bar;
using Surveys.WPF.Shared.Navigation.Modal;

namespace Surveys.WPF.Shared.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ModalNavigationMediator _modalNavigationMediator;
    private readonly NavigationMediator _navigationMediator;

    public MainViewModel(
        ModalNavigationMediator modalNavigationMediator,
        NavigationMediator navigationMediator,
        NavigationBarViewModel barViewModel)
    {
        _modalNavigationMediator = modalNavigationMediator;
        _navigationMediator = navigationMediator;

        barViewModel.MainViewModel = this;
        NavigationBarViewModel = barViewModel;

        _modalNavigationMediator.CurrentViewModelChanged += OnCurrentModalViewModalChanged;
        _navigationMediator.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public NavigationBarViewModel NavigationBarViewModel { get; }
    public ViewModelBase? CurrentViewModel => _navigationMediator.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationMediator.CurrentViewModel;
    public bool IsOpen => _modalNavigationMediator.IsOpen;

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
        _modalNavigationMediator.Close();
    }

    private void OnCurrentModalViewModalChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsOpen));
    }

    public override void Dispose()
    {
        _modalNavigationMediator.CurrentViewModelChanged -= OnCurrentModalViewModalChanged;
        _navigationMediator.CurrentViewModelChanged -= OnCurrentViewModelChanged;

        base.Dispose();
    }
}