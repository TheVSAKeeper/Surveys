namespace Surveys.WPF.Application.Commands;

internal class LambdaCommand(Action<object> execute, Func<object, bool>? canExecute = null) : Command
{
    private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public override bool CanExecute(object? parameter) => parameter != null && (canExecute?.Invoke(parameter) ?? true);

    public override void Execute(object? parameter)
    {
        if (CanExecute(parameter) == false)
            return;

        if (parameter != null)
            _execute(parameter);
    }
}