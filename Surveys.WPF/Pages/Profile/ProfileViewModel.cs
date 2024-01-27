using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain.Base;
using Surveys.Infrastructure;
using Surveys.WPF.Endpoints.AuthenticationEndpoints;
using Surveys.WPF.Endpoints.AuthenticationEndpoints.Register;
using Surveys.WPF.Endpoints.AuthenticationEndpoints.Update;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Profile;

public class ProfileViewModel(
    AuthenticationManager authenticationManager,
    NavigationService<HomeViewModel> homeNavigationService,
    ApplicationRoleStore roleStore,
    IMediator mediator,
    IMapper mapper)
    : ViewModelBase
{
    public ApplicationUserUpdateViewModel ApplicationUserUpdateViewModel { get; } = new(authenticationManager, mediator, mapper);
    public RegisterFormViewModel RegisterFormViewModel { get; } = new(authenticationManager, roleStore);

    public bool IsUserAdministrator => authenticationManager.IsInRole(AppData.SystemAdministratorRoleName);

    public ICommand NavigateHomeCommand { get; } = new NavigateCommand(homeNavigationService);
}