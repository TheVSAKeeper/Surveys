using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Endpoints.AnamnesisTemplateEndpoints.Components;
using Surveys.Blazor.Services;
using Surveys.Web.Application.Messaging.AnamnesisMessages;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.Pages;

public partial class SurveyUpdate
{
    private AnamnesisTemplateModal _anamnesisTemplateModal;
    private Validations? _fluentValidations;

    [Parameter] public Guid Id { get; set; }
    [Inject] private IAuthorizedHttpClient Client { get; set; } = null!;

    private SurveyUpdateViewModel? ViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadSurveyUpdateViewModel();
    }

    private async Task LoadSurveyUpdateViewModel()
    {
        Operation<SurveyUpdateViewModel>? result = await Client.GetFromJsonAsync<SurveyUpdateViewModel>($"surveys/get-for-edit/{Id}");

        if (result is { Ok: true })
            ViewModel = result.Result;
    }

    private async Task HandleTemplatesSelected(List<AnamnesisTemplateViewModel> obj)
    {
        if (ViewModel == null)
            return;

        foreach (AnamnesisTemplateViewModel template in obj)
        {
            await Client.PostAsync("anamneses", new AnamnesisCreateViewModel
            {
                SurveyId = ViewModel.Id,
                AnamnesisTemplateId = template.Id
            });
        }

        await LoadSurveyUpdateViewModel();
    }

    private void OnSaveSurveyClicked(MouseEventArgs obj)
    {
        if (ViewModel == null)
            return;

        string serializedViewModel = JsonConvert.SerializeObject(ViewModel, Formatting.Indented);
        Console.WriteLine(serializedViewModel);
    }

    private void OnAnswerChanged(ResponseViewModel obj)
    {
        AnamnesisViewModel? anamnesisViewModel = ViewModel?.Anamneses.FirstOrDefault(x => x.Id == obj.AnamnesisId);

        if (anamnesisViewModel != null)
        {
            anamnesisViewModel.Responses = [obj];
            Console.WriteLine(JsonConvert.SerializeObject(anamnesisViewModel, Formatting.Indented));
        }
    }
}