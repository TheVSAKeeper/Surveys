using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Endpoints.AuthenticationEndpoints.Logout;

public class LogoutCommand(AuthenticationManager authenticationManager, INavigationService loginNavigationService)
    : CommandBase
{
    protected override void Execute(object? parameter)
    {
        authenticationManager.SignOut();

        loginNavigationService.Navigate();
    }
}