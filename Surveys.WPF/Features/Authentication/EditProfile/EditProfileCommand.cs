using Calabonga.UnitOfWork;
using Surveys.Infrastructure;
using Surveys.WPF.Features.Authentication.ViewProfile;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Authentication.EditProfile;

public class EditProfileCommand(ProfileDetailsViewModel profileDetailsViewModel, IUnitOfWork unitOfWork)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        await unitOfWork.GetRepository<ApplicationUser>().InsertAsync(profileDetailsViewModel.User);
        await unitOfWork.SaveChangesAsync();
        /*IdentityResult result = await authenticationStore.UpdateUserAsync(profileDetailsViewModel.User);

        if (result.Succeeded == false)
        {
            string errors = string.Join(Environment.NewLine, result.Errors.Select(error => error.Description));
            MessageBox.Show($"Ошибка изменения. {Environment.NewLine}{errors}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("Успешно изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);*/
    }
}