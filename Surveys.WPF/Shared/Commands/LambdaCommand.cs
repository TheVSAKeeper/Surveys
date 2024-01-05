namespace Surveys.WPF.Shared.Commands;

public class LambdaCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : CommandBase
{
    private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public override bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

    public override void Execute(object? parameter)
    {
        if (CanExecute(parameter) == false)
            return;

        if (parameter != null)
            _execute(parameter);
    }
}