using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.EditProfile;

public class ProfileDetailsViewModel : ViewModelBase
{
    private readonly AuthenticationStore _authenticationStore;
    private UserUpdateViewModel _user;

    public ProfileDetailsViewModel(AuthenticationStore authenticationStore, IMediator mediator, IMapper mapper)
    {
        _authenticationStore = authenticationStore;
        SubmitCommand = new EditProfileCommand(this, mediator);
        _user = mapper.Map<UserUpdateViewModel>(authenticationStore.User);
    }

    public UserUpdateViewModel User
    {
        get => _user;
        set => Set(ref _user, value);
    }

    public ICommand SubmitCommand { get; }
}