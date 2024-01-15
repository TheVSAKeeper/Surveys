namespace Surveys.WPF.Shared.Commands;

public class LambdaCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : CommandBase
{
    private readonly Action<object?> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public LambdaCommand(Action execute, Func<bool>? canExecute = null)
        : this(p => execute(), canExecute is null ? null : _ => canExecute())
    {
    }

    protected override bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

    protected override void Execute(object? parameter) => _execute(parameter);
}