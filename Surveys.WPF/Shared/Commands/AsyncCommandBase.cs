namespace Surveys.WPF.Shared.Commands;

public abstract class AsyncCommandBase(Action<Exception>? onException = null) : CommandBase
{
    private bool _isExecuting;

    private bool IsExecuting
    {
        get => _isExecuting;
        set
        {
            _isExecuting = value;
            OnCanExecuteChanged();
        }
    }

    protected sealed override async void Execute(object? parameter)
    {
        IsExecuting = true;

        try
        {
            await ExecuteAsync(parameter);
        }
        catch (Exception ex)
        {
            onException?.Invoke(ex);
        }

        IsExecuting = false;
    }

    protected sealed override bool CanExecute(object? parameter) => IsExecuting == false && CanExecuteAsync(parameter);

    protected abstract Task ExecuteAsync(object? parameter);

    protected virtual bool CanExecuteAsync(object? parameter) => true;
}