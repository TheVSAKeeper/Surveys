using System.Windows.Input;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Home
{
    public class HomeViewModel : ViewModelBase
    {
      //  private readonly CurrentUserStore _currentUserStore;

      //  public string Username => _currentUserStore.User?.DisplayName ?? "Unknown";

        public ICommand NavigateProfileCommand { get; }
      //  public ICommand LogoutCommand { get; }

        public HomeViewModel(
            //AuthenticationStore authenticationStore, 
           // CurrentUserStore currentUserStore,
            INavigationService profileNavigationService,
            INavigationService loginNavigationService)
        {
          //  _currentUserStore = currentUserStore;

            NavigateProfileCommand = new NavigateCommand(profileNavigationService);
           // LogoutCommand = new LogoutCommand(authenticationStore, loginNavigationService);
        }

        public static HomeViewModel LoadViewModel(
         //   AuthenticationStore authenticationStore, 
      //      CurrentUserStore currentUserStore,
            INavigationService profileNavigationService,
            INavigationService loginNavigationService)
        {
            HomeViewModel homeViewModel = new HomeViewModel(
                //authenticationStore,
         //       currentUserStore,
          //      getSecretMessageQuery,
                profileNavigationService,
                loginNavigationService);

           // homeViewModel.LoadSecretMessageCommand.Execute(null);

            return homeViewModel;
        }
    }
}
