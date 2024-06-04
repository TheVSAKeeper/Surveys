using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Surveys.Blazor.Endpoints.AuthenticationEndpoints.Components;

public partial class LoginDisplay
{
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    public void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }

    public void BeginLogIn()
    {
        Navigation.NavigateToLogout("authentication/login");
    }
}