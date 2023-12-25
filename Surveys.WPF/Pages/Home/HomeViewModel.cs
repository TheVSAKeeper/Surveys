using System.Windows.Input;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Logout;
using Surveys.WPF.Pages.Profile;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Home;

public class HomeViewModel : ViewModelBase
{
    private readonly AuthenticationStore _authenticationStore;

    public HomeViewModel(
        AuthenticationStore authenticationStore,
        INavigationService profileNavigationService,
        INavigationService loginNavigationService)
    {
        _authenticationStore = authenticationStore;

        NavigateProfileCommand = new NavigateCommand(profileNavigationService);
        LogoutCommand = new LogoutCommand(authenticationStore, loginNavigationService);
    }

    public ICommand NavigateProfileCommand { get; }
    public ICommand LogoutCommand { get; }

    public string Username => _authenticationStore.User?.DisplayName ?? "Unknown";

    public static HomeViewModel LoadViewModel(
        AuthenticationStore authenticationStore,
        NavigationService<ProfileViewModel> profileNavigationService,
        INavigationService loginNavigationService
    )
    {
        HomeViewModel homeViewModel = new(authenticationStore, profileNavigationService, loginNavigationService);

        return homeViewModel;
    }
}