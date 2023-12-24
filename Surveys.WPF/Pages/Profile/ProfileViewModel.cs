using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.ViewProfile;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Profile;

public class ProfileViewModel : ViewModelBase
{
    public ProfileViewModel(AuthenticationStore authenticationStore, INavigationService homeNavigationService)
    {
        ProfileDetailsViewModel = new ProfileDetailsViewModel(authenticationStore, homeNavigationService);
    }

    public ProfileDetailsViewModel ProfileDetailsViewModel { get; }
}