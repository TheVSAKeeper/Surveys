namespace Surveys.WPF.Shared.Navigation.Modal;

public class CloseModalNavigationService(ModalNavigationMediator navigationMediator) : INavigationService
{
    public void Navigate()
    {
        navigationMediator.Close();
    }
}