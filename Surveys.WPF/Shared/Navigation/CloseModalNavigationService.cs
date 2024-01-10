namespace Surveys.WPF.Shared.Navigation;

public class CloseModalNavigationService(ModalNavigationStore navigationStore) : INavigationService
{
    public void Navigate()
    {
        navigationStore.Close();
    }
}