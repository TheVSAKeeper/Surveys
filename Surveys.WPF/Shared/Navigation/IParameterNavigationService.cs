namespace Surveys.WPF.Shared.Navigation;

public interface IParameterNavigationService<in TParameter>
{
    void Navigate(TParameter parameter);
}