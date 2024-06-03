using Blazorise;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Endpoints.SurveyEndpoints.ViewModels;
using Surveys.Blazor.Services;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.Pages;

public partial class SurveyUpdate
{
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
        Operation<SurveyUpdateViewModel>? result = await Client.GetFromJsonAsync<Operation<SurveyUpdateViewModel>>($"surveys/get-for-edit/{Id}");

        if (result is { Ok: true })
            ViewModel = result.Result;
    }

}