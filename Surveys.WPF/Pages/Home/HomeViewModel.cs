using System.Windows.Input;
using Surveys.WPF.Endpoints.AuthenticationEndpoints;
using Surveys.WPF.Endpoints.AuthenticationEndpoints.Logout;
using Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Pages.Profile;
using Surveys.WPF.Pages.Survey;
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
        NavigationService<SurveyViewModel> surveyNavigationService,
        SurveyShowAllFormViewModel showAllFormViewModel)
    {
        _authenticationStore = authenticationStore;

        NavigateProfileCommand = new NavigateCommand(profileNavigationService);
        OpenSurveysCommand = new NavigateCommand(surveyNavigationService);
        LogoutCommand = new LogoutCommand(authenticationStore, loginNavigationService);
        SurveyShowAllFormViewModel = showAllFormViewModel;
    }

    public SurveyShowAllFormViewModel SurveyShowAllFormViewModel { get; }
    public ICommand NavigateProfileCommand { get; }
    public ICommand OpenSurveysCommand { get; }
    public ICommand LogoutCommand { get; }

    public string Username => _authenticationStore.User?.DisplayName ?? "Unknown";
}