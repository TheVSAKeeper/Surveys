using System.Windows.Input;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.ViewProfile;

public class ProfileDetailsViewModel : ViewModelBase
{
    private readonly AuthenticationStore _authenticationStore;

    public ProfileDetailsViewModel(
        AuthenticationStore authenticationStore,
        INavigationService homeNavigationService)
    {
        _authenticationStore = authenticationStore;

        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
    }

    public string Username => _authenticationStore.User?.DisplayName ?? string.Empty;
    public string Email => _authenticationStore.User?.Email ?? string.Empty;

    public ICommand NavigateHomeCommand { get; }
}