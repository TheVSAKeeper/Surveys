using System.Net.Http.Json;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Services;
using Surveys.Domain;
using Surveys.Web.Application.Messaging.AnamnesisMessages;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.Components;

public partial class SurveyCreateModal
{
    private bool _isDefaultSurvey;
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
        {
            _createViewModel.Id = survey.Result.Id;

            if (_isDefaultSurvey)
                await FillBaseSurvey(_createViewModel.Id);
        }

        if (response.IsSuccessStatusCode)
            await MessageService.Success("Опрос успешно создан!", "Создание опроса");
        else
            await MessageService.Error($"Не удалось создать опрос. Код ошибки: {response.StatusCode}", "Создание опроса");

        await OnSubmitComplaint.InvokeAsync(_createViewModel);
        await _modal.Hide();
    }

    private async Task FillBaseSurvey(Guid id)
    {
        Operation<PagedListResult<AnamnesisTemplateViewModel>>? response = await Client
            .GetFromJsonAsync<PagedListResult<AnamnesisTemplateViewModel>>($"anamnesis-template/paged/{0}?pageSize={999}");

        if (response == null || response.Ok == false)
            return;

        List<AnamnesisTemplateViewModel> templates = [..response.Result.Items.ToList()];

        if (_createViewModel is { Patient: not null } && _createViewModel.Patient.Gender != Gender.Female)
            templates = templates.Where(model => string.Equals(model.Title, "Гинекологический анамнез", StringComparison.InvariantCultureIgnoreCase) == false).ToList();

        foreach (AnamnesisTemplateViewModel template in templates)
        {
            await Client.PostAsync("anamneses", new AnamnesisCreateViewModel
            {
                SurveyId = id,
                AnamnesisTemplateId = template.Id
            });
        }
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