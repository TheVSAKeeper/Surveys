using System.Windows;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.Update;

public class ApplicationUserUpdateViewModel : ViewModelBase
{
    private ApplicationUserUpdateDto _applicationUser;

    public ApplicationUserUpdateViewModel(AuthenticationStore authenticationStore, IMediator mediator, IMapper mapper)
    {
        SubmitCommand = new ApplicationUserUpdateCommand(this, mediator, authenticationStore);
        _applicationUser = mapper.Map<ApplicationUserUpdateDto>(authenticationStore.User);
    }

    public ApplicationUserUpdateDto ApplicationUser
    {
        get => _applicationUser;
        set => Set(ref _applicationUser, value);
    }

    public ICommand SubmitCommand { get; }
}