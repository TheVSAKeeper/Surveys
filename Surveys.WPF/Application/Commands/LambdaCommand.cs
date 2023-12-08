namespace Surveys.WPF.Application.Commands;

internal class LambdaCommand(Action<object> execute, Func<object, bool>? canExecute = null) : Command
{
    private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public override bool CanExecute(object? parameter) => canExecute == null || canExecute.Invoke(parameter);

    public override void Execute(object? parameter)
    {
        if (CanExecute(parameter) == false)
            return;

        _execute(parameter);
    }
}