using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Features.Authentication.Logout;

public class LogoutCommand : CommandBase
{
    private readonly AuthenticationStore _authenticationStore;
    private readonly INavigationService _loginNavigationService;

    public LogoutCommand(AuthenticationStore authenticationStore, INavigationService loginNavigationService)
    {
        _authenticationStore = authenticationStore;
        _loginNavigationService = loginNavigationService;
    }

    public override void Execute(object? parameter)
    {
        _authenticationStore.SignOutAsync();

        _loginNavigationService.Navigate();
    }
}