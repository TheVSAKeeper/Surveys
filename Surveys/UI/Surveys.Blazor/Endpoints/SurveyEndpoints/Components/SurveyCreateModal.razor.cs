using System.Net.Http.Json;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Endpoints.PatientEndpoints.ViewModels;
using Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;
using Surveys.Blazor.Services;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.Components;

public partial class SurveyCreateModal
{
    private Modal _modal = null!;
    private SurveyCreateViewModel? _createViewModel;
    private Validations? _fluentValidations;

    [Parameter] public EventCallback<SurveyCreateViewModel> OnSubmitComplaint { get; set; }

    [Inject] public IAuthorizedHttpClient Client { get; set; } = null!;
    [Inject] private IMessageService MessageService { get; set; } = null!;

    private async Task SubmitComplaint()
    {
        if (_fluentValidations != null && await _fluentValidations.ValidateAll() == false)
            return;

        HttpResponseMessage response = await Client.PostAsync("surveys", _createViewModel);
        Operation<SurveyViewModel>? survey = await response.Content.ReadFromJsonAsync<Operation<SurveyViewModel>>();

        if (survey is { Ok: true } && _createViewModel is not null)
            _createViewModel.Id = survey.Result.Id;

        if (response.IsSuccessStatusCode)
            await MessageService.Success("Опрос успешно создан!", "Создание опроса");
        else
            await MessageService.Error($"Не удалось создать опрос. Код ошибки: {response.StatusCode}", "Создание опроса");

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