using System.Windows.Input;
using Surveys.Infrastructure;
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
        User = authenticationStore.User;

        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
    }

    private ApplicationUser? _user;

    public ApplicationUser? User
    {
        get => _user;
        set => Set(ref _user, value);
    }
    public ICommand NavigateHomeCommand { get; }
}