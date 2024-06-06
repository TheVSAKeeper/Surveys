using Blazorise;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Services;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.AnamnesisTemplateEndpoints.Components;

public partial class AnamnesisTemplateModal
{
    private Modal _modal = null!;
    private List<AnamnesisTemplateViewModel> Templates { get; set; } = [];
    private List<AnamnesisTemplateViewModel> SelectedTemplates { get; set; } = [];

    [Parameter] public EventCallback<List<AnamnesisTemplateViewModel>> OnTemplatesSelected { get; set; }

    [Inject] public IAuthorizedHttpClient Client { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await Load();
        await base.OnInitializedAsync();
    }

    private async Task Load()
    {
        Operation<PagedListResult<AnamnesisTemplateViewModel>>? response = await Client
            .GetFromJsonAsync<PagedListResult<AnamnesisTemplateViewModel>>($"anamnesis-template/paged/{0}?pageSize={999}");

        if (response?.Ok ?? false)
            Templates = [..response.Result.Items.ToList()];
    }

    public void Show()
    {
        _modal.Show();
    }

    private void SaveAndClose()
    {
        SelectedTemplates = Templates.Where(t => t.IsSelected).ToList();
        OnTemplatesSelected.InvokeAsync(SelectedTemplates);
        CloseModal();
    }

    private void CloseModal()
    {
        _modal.Hide();
    }
}