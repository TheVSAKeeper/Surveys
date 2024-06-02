using Blazorise;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Endpoints.PatientEndpoints.ViewModels;
using Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.Components;

public partial class SurveyCreateModal
{
    private Modal _modal = null!;
    private SurveyCreateViewModel? _createViewModel;
    private Validations? _fluentValidations;

    [Parameter] public EventCallback<SurveyCreateViewModel> OnSubmitComplaint { get; set; }

    private async Task SubmitComplaint()
    {
        if (_fluentValidations != null && await _fluentValidations.ValidateAll() == false)
            return;

        await OnSubmitComplaint.InvokeAsync(_createViewModel);
        await _modal.Hide();
    }

    public void Show(PatientViewModel patient)
    {
        _createViewModel = new SurveyCreateViewModel
        {
            PatientId = patient.Id,
            Patient = patient,
            Complaint = string.Empty
        };

        _modal.Show();
    }

    public async Task Hide()
    {
        await _modal.Hide();
    }
}