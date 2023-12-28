namespace Surveys.WPF.Shared.ViewModels;

public delegate TViewModel CreateViewModel<out TViewModel>() where TViewModel : ViewModelBase;

public delegate TViewModel CreateViewModel<in TParameter, out TViewModel>(TParameter parameter) where TViewModel : ViewModelBase;