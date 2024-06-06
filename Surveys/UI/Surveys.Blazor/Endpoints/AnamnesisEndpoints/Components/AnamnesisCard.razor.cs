using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Services;
using Surveys.Web.Application.Messaging.AnamnesisMessages;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.Components;

public partial class AnamnesisCard
{
    [Parameter] public required AnamnesisViewModel Anamnesis { get; set; }

    [Parameter] public EventCallback AnamnesisChanged { get; set; }

    [Inject] public IAuthorizedHttpClient Client { get; set; } = null!;

    private AnamnesisTemplateViewModel? AnamnesisTemplate => Anamnesis.AnamnesisTemplate;

    private async Task OnAnswerChanged()
    {
        /*Anamnesis = null;
        Operation<AnamnesisViewModel>? result = await Client.GetFromJsonAsync<AnamnesisViewModel>($"anamneses/{Anamnesis.Id}");
        Anamnesis = result.Result;*/

        await AnamnesisChanged.InvokeAsync();
    }
}