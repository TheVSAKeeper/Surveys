using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.AuthenticationEndpoints.Logout;

public class LogoutCommand(AuthenticationStore authenticationStore, INavigationService loginNavigationService)
    : CommandBase
{
    protected override void Execute(object? parameter)
    {
        authenticationStore.SignOut();

        loginNavigationService.Navigate();
    }
}