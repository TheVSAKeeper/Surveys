using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Endpoints.SurveyEndpoints.Components;
using Surveys.Blazor.Services;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.PatientEndpoints.Pages;

public partial class Patients
{
    private int _totalPatients;
    private List<PatientViewModel> _patients = null!;
    private SurveyCreateModal? _surveyCreateModal;

    private PatientViewModel? SelectedPatient { get; set; }

    [Inject] private IAuthorizedHttpClient Client { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _patients = [..(await GetPaged(0)).Items];
        await base.OnInitializedAsync();
    }

    private void ShowComplaintModal(PatientViewModel patient)
    {
        _surveyCreateModal?.Show(patient);
    }

    private void SubmitComplaint(SurveyCreateViewModel createViewModel)
    {
        Navigation.NavigateTo($"/survey-update/{createViewModel.Id}");
    }

    private async Task<PagedListResult<PatientViewModel>> GetPaged(int pageIndex, int pageSize = 10)
    {
        Operation<PagedListResult<PatientViewModel>>? response = await Client
            .GetFromJsonAsync<PagedListResult<PatientViewModel>>($"patients/paged/{pageIndex}?pageSize={pageSize}");

        return response?.Result
               ?? new PagedListResult<PatientViewModel>
               {
                   Items = []
               };
    }

    private async Task OnReadData(DataGridReadDataEventArgs<PatientViewModel> eventArgs)
    {
        if (eventArgs.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        PagedListResult<PatientViewModel> collection = await GetPaged(eventArgs.Page - 1, eventArgs.PageSize);

        _totalPatients = collection.TotalCount;
        _patients = [..collection.Items];
    }
}
