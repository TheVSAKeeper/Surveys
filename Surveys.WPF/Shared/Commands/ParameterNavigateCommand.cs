using Surveys.Domain.Exceptions;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Shared.Commands;

public class ParameterNavigateCommand<T>(IParameterNavigationService<T> navigationService) : CommandBase
{
    protected override void Execute(object? parameter)
    {
        navigationService.Navigate((T)parameter! ?? throw new SurveysArgumentNullException(nameof(parameter)));
    }
}