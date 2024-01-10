namespace Surveys.WPF.Shared.Navigation.Modal;

public class CloseModalNavigationService(ModalNavigationStore navigationStore) : INavigationService
{
    public void Navigate()
    {
        navigationStore.Close();
    }
}