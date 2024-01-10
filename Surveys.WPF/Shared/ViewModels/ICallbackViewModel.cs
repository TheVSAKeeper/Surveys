namespace Surveys.WPF.Shared.ViewModels;

public interface ICallbackViewModel<out T>
{
    public void SetCallback(Action<T> callback);
}