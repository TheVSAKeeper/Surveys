using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Endpoints.PatientEndpoints.ViewModels;
using Surveys.Blazor.Services;

namespace Surveys.Blazor.Endpoints.PatientEndpoints.Pages;

public partial class Patients
{
    private int _totalPatients;

    private List<PatientViewModel> _patients = null!;

    [Inject] private AuthorizedHttpClient Client { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _patients = [..(await GetPaged(0)).Items];
        await base.OnInitializedAsync();
    }

    private async Task<PagedListResult<PatientViewModel>> GetPaged(int pageIndex, int pageSize = 10)
    {
        Operation<PagedListResult<PatientViewModel>>? response = await Client
            .GetFromJsonAsync<Operation<PagedListResult<PatientViewModel>>>($"https://localhost:10001/api/patients/paged/{pageIndex}?pageSize={pageSize}");

        return response?.Result
               ?? new PagedListResult<PatientViewModel>
               {
                   Items = []
               };
    }

    private async Task OnReadData(DataGridReadDataEventArgs<PatientViewModel> eventArgs)
    {
        if (eventArgs.CancellationToken.IsCancellationRequested)
            return;

        PagedListResult<PatientViewModel> collection = await GetPaged(eventArgs.Page - 1, eventArgs.PageSize);

        _totalPatients = collection.TotalCount;
        _patients = [..collection.Items];
    }
}