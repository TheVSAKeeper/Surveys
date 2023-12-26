using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Authentication.Update;

public class ApplicationUserUpdateViewModel : ViewModelBase
{
    private readonly ApplicationUserUpdateDtoValidator _validator = new();
    private ApplicationUserUpdateDto _applicationUser;

    private ObservableCollection<string> _errors = [];

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

    public ObservableCollection<string> Errors
    {
        get => _errors;
        set => Set(ref _errors, value);
    }

    public bool HasErrors => Errors.Count != 0;

    public void Validate()
    {
        Errors.Clear();

        ValidationResult result = _validator.Validate(ApplicationUser);

        IEnumerable<ValidationFailure> errors = result.Errors.DistinctBy(e => e.PropertyName);

        foreach (string error in errors.Select(failure => failure.ErrorMessage))
            Errors.Add(error);

        OnPropertyChanged(nameof(Errors));
        OnPropertyChanged(nameof(HasErrors));
    }
}