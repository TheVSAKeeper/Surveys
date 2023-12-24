using System.Windows.Markup;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Home;

[MarkupExtensionReturnType(typeof(HomeViewModel))]
public class HomeViewModel : ViewModelBase
{
    private readonly AuthenticationStore _authenticationStore;
    //  private readonly CurrentUserStore _currentUserStore;

      public string Username => _authenticationStore.User?.Email ?? "Unknown";

    //   public ICommand NavigateProfileCommand { get; }
    //  public ICommand LogoutCommand { get; }

    public HomeViewModel(
        AuthenticationStore authenticationStore,
        // CurrentUserStore currentUserStore,
        //INavigationService profileNavigationService,
        INavigationService loginNavigationService)
    {
        _authenticationStore = authenticationStore;
        //  _currentUserStore = currentUserStore;

        //  NavigateProfileCommand = new NavigateCommand(profileNavigationService);
        // LogoutCommand = new LogoutCommand(authenticationStore, loginNavigationService);
    }

    public static HomeViewModel LoadViewModel(
        AuthenticationStore authenticationStore,
        //      CurrentUserStore currentUserStore,
        // INavigationService profileNavigationService,
        INavigationService loginNavigationService)
    {
        HomeViewModel homeViewModel = new(authenticationStore,
            //       currentUserStore,
            //      getSecretMessageQuery,
            //    profileNavigationService,
            loginNavigationService);

        // homeViewModel.LoadSecretMessageCommand.Execute(null);

        return homeViewModel;
    }
}