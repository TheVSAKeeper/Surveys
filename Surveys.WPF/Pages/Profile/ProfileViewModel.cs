using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain.Base;
using Surveys.Infrastructure;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Features.Authentication.Register;
using Surveys.WPF.Features.Authentication.Update;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Pages.Profile;

public class ProfileViewModel(
    AuthenticationStore authenticationStore,
    NavigationService<HomeViewModel> homeNavigationService,
    ApplicationRoleStore roleStore,
    IMediator mediator,
    IMapper mapper)
    : ViewModelBase
{
    public ApplicationUserUpdateViewModel ApplicationUserUpdateViewModel { get; } = new(authenticationStore, mediator, mapper);
    public RegisterFormViewModel RegisterFormViewModel { get; } = new(authenticationStore, roleStore);

    public bool IsUserAdministrator => authenticationStore.IsInRole(AppData.SystemAdministratorRoleName);

    public ICommand NavigateHomeCommand { get; } = new NavigateCommand(homeNavigationService);
}