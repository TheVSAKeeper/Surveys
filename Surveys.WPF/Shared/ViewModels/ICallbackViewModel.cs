namespace Surveys.WPF.Shared.ViewModels;

public interface ICallbackViewModel<out T>
{
    public event Action<T> Callback;
}