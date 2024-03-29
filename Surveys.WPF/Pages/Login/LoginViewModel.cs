﻿using Surveys.WPF.Endpoints.AuthenticationEndpoints;
using Surveys.WPF.Endpoints.AuthenticationEndpoints.Login;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Login;

public class LoginViewModel(AuthenticationManager authenticationManager, NavigationService<HomeViewModel> homeNavigationService)
    : ViewModelBase
{
    public LoginFormViewModel LoginFormViewModel { get; } = new(authenticationManager, homeNavigationService);
}