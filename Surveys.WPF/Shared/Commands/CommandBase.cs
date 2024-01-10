using System.Windows.Input;

namespace Surveys.WPF.Shared.Commands;

public abstract class CommandBase : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

    void ICommand.Execute(object? parameter)
    {
        if (CanExecute(parameter))
            Execute(parameter);
    }

    protected virtual bool CanExecute(object? parameter) => true;

    protected void OnCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }

    protected abstract void Execute(object? parameter);
}