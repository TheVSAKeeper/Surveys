using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.Navigation.Modal;

namespace Surveys.WPF.Shared.Commands;

public class NavigateCommand(INavigationService navigationService) : CommandBase
{
    protected override void Execute(object? parameter)
    {
        navigationService.Navigate();
    }
}

public class CallbackNavigateCommand<T>(ICallbackNavigationService<T> navigationService, Action<T> callback) : CommandBase
{
    protected override void Execute(object? parameter)
    {
        navigationService.Navigate(callback);
    }
}