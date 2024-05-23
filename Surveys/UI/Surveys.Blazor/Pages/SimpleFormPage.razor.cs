using Blazorise;

namespace Surveys.Blazor.Pages;

public partial class SimpleFormPage
{
    private string? _password;
    private Validations? _validationsBasicExampleRef;
    private Validations? _validationsHorizontalFormRef;

    private void ValidatePassword(ValidatorEventArgs e)
    {
        e.Status = Convert.ToString(e.Value)?.Length >= 6 ? ValidationStatus.Success : ValidationStatus.Error;
    }

    private void ValidatePassword2(ValidatorEventArgs e)
    {
        string? password2 = Convert.ToString(e.Value);

        if (password2?.Length < 6)
        {
            e.Status = ValidationStatus.Error;
            e.ErrorText = "Password must be at least 6 characters long!";
        }
        else if (password2 != _password)
        {
            e.Status = ValidationStatus.Error;
        }
        else
        {
            e.Status = ValidationStatus.Success;
        }
    }

    private async Task SubmitBasicExample()
    {
        if (await _validationsBasicExampleRef!.ValidateAll())
            await _validationsBasicExampleRef.ClearAll();
    }

    private async Task SubmitHorizontalForm()
    {
        if (await _validationsHorizontalFormRef!.ValidateAll())
            await _validationsHorizontalFormRef.ClearAll();
    }
}