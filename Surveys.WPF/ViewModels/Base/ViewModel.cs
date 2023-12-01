using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace Surveys.WPF.ViewModels.Base;

public abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
{
    private bool _disposed;
    private WeakReference _rootRef;

    private WeakReference _targetRef;

    public object TargetObject => _targetRef.Target;

    public object RootObject => _rootRef.Target;

    //~ViewModel()
    //{
    //    Dispose(false);
    //}

    public void Dispose()
    {
        Dispose(true);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        /*PropertyChangedEventHandler? handlers = PropertyChanged;

        if (handlers is null)
            return;

        Delegate[] invocationList = handlers.GetInvocationList();

        PropertyChangedEventArgs arg = new PropertyChangedEventArgs(propertyName);

        foreach (Delegate action in invocationList)
        {
            if (action.Target is DispatcherObject dispObject)
                dispObject.Dispatcher.Invoke(action, this, arg);
            else
                action.DynamicInvoke(this, arg);
        }*/
    }

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public override object ProvideValue(IServiceProvider sp)
    {
        IProvideValueTarget? valueTargetService = sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
        IRootObjectProvider? rootObjectService = sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

        OnInitialized(valueTargetService?.TargetObject,
            valueTargetService?.TargetProperty,
            rootObjectService?.RootObject);

        return this;
    }

    protected virtual void OnInitialized(object target, object property, object root)
    {
        _targetRef = new WeakReference(target);
        _rootRef = new WeakReference(root);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || _disposed)
            return;

        _disposed = true;
    }
}