using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Shared.Commands;

public class NavigateCommand(INavigationService navigationService) : CommandBase
{
    protected override void Execute(object? parameter)
    {
        navigationService.Navigate();
    }
}