using System.Windows.Input;
using Calabonga.UnitOfWork;
using Surveys.Infrastructure;
using Surveys.WPF.Features.Authentication.EditProfile;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.ViewProfile;

public class ProfileDetailsViewModel : ViewModelBase
{
    private readonly AuthenticationStore _authenticationStore;
    private ApplicationUser? _user;

    public ProfileDetailsViewModel(AuthenticationStore authenticationStore, IUnitOfWork unitOfWork)
    {
        _authenticationStore = authenticationStore;
        SubmitCommand = new EditProfileCommand(this, unitOfWork);
        User = authenticationStore.User;
    }

    public ApplicationUser? User
    {
        get => _user;
        set => Set(ref _user, value);
    }

    public ICommand SubmitCommand { get; }

}