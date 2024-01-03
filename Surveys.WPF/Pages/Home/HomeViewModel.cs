using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.WPF.Features.Anamnesis.Create;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Logout;
using Surveys.WPF.Features.Creation.Create;
using Surveys.WPF.Pages.Login;
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
        NavigationService<ProfileViewModel> profileNavigationService,
        NavigationService<LoginViewModel> loginNavigationService,
        IMediator mediator,
        IMapper mapper)
    {
        _authenticationStore = authenticationStore;
        AnamnesesCreateFormViewModel = new AnamnesesCreateFormViewModel(mediator, mapper);

        NavigateProfileCommand = new NavigateCommand(profileNavigationService);
        LogoutCommand = new LogoutCommand(authenticationStore, loginNavigationService);
    }

    public ICommand NavigateProfileCommand { get; }
    public ICommand LogoutCommand { get; }

    public string Username => _authenticationStore.User?.DisplayName ?? "Unknown";

    public AnamnesesCreateFormViewModel AnamnesesCreateFormViewModel { get; }
}