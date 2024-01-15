namespace Surveys.WPF.Shared.ViewModels;

public interface IParameterViewModel<in T>
{
    public void SetParameter(T parameter);
}