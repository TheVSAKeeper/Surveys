namespace Surveys.WPF.Shared.Commands;

public abstract class AsyncCommandBase(Action<Exception>? onException = null) : CommandBase
{
    public bool IsExecuting { get; private set; }

    public override bool CanExecute(object? parameter) => IsExecuting == false && base.CanExecute(parameter);

    public override async void Execute(object? parameter)
    {
        IsExecuting = true;

        try
        {
            await ExecuteAsync(parameter);
        }
        catch (Exception exception)
        {
            onException?.Invoke(exception);
        }

        IsExecuting = false;
    }

    protected abstract Task ExecuteAsync(object? parameter);
}