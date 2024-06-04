using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Services;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.SurveyEndpoints.Components;

public partial class PatientSurveys
{
    private const string PREVIOUS = "previous";
    private const string NEXT = "next";

    private int _currentPage = 1;
    private int _pageSize = 5;
    private int _totalPages = 1;
    private int[] _pageSizes = [1, 2, 3, 5, 10];

    private List<SurveyViewModel>? _surveys;

    [Parameter] public Guid PatientId { get; set; }
    [Inject] private IAuthorizedHttpClient Client { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadSurveys(1);
        await base.OnInitializedAsync();
    }

    private async Task LoadSurveys(int pageIndex)
    {
        _surveys = [..(await GetPaged(pageIndex - 1, _pageSize)).Items];
        _currentPage = pageIndex;
    }

    private async Task<PagedListResult<SurveyViewModel>> GetPaged(int pageIndex, int pageSize = 10)
    {
        Operation<PagedListResult<SurveyViewModel>>? response = await Client
            .GetFromJsonAsync<Operation<PagedListResult<SurveyViewModel>>>($"surveys/paged/{pageIndex}?pageSize={pageSize}&patientId={PatientId}");

        if (response?.Result != null)
        {
            _totalPages = response.Result.TotalPages;
            return response.Result;
        }

        return new PagedListResult<SurveyViewModel>
        {
            Items = new List<SurveyViewModel>(),
            TotalCount = 0
        };
    }

    private async Task Previous()
    {
        if (_currentPage > 1)
            await LoadSurveys(_currentPage - 1);
    }

    private async Task Next()
    {
        if (_currentPage < _totalPages)
            await LoadSurveys(_currentPage + 1);
    }

    private bool IsPageNavigationDisabled(string navigation)
    {
        return navigation switch
        {
            PREVIOUS => _currentPage == 1,
            NEXT => _currentPage == _totalPages,
            var _ => false
        };
    }

    private async Task OnPageSizeChanged(int pageSize)
    {
        _pageSize = pageSize;
        await LoadSurveys(1);
    }

    private async Task SetActive(string page) => await LoadSurveys(Convert.ToInt32(page));

    private bool IsActive(int page) => _currentPage == page;
}