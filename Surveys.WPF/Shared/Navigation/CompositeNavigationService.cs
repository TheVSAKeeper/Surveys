namespace Surveys.WPF.Shared.Navigation;

public class CompositeNavigationService(params INavigationService[] navigationServices) : INavigationService
{
    private readonly IEnumerable<INavigationService> _navigationServices = navigationServices;

    public void Navigate()
    {
        foreach (INavigationService navigationService in _navigationServices)
            navigationService.Navigate();
    }
}