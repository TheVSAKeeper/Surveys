using Surveys.Blazor.Domain;

namespace Surveys.Blazor.Services;

public interface IAuthorizedHttpClient
{
    Task<HttpResponseMessage> GetAsync(string url);
    Task<HttpResponseMessage> PostAsync<T>(string url, T data);
    Task<HttpResponseMessage> PutAsync<T>(string url, T data);
    Task<HttpResponseMessage> DeleteAsync(string url);
    Task<Operation<T>?> GetFromJsonAsync<T>(string url);
    Task<Operation<PagedListResult<T>>?> GetPagedAsync<T>(string url, int pageIndex = 0, int pageSize = 10, string search = "");
    Task<Operation<TR>?> PostFromJsonAsync<T, TR>(string url, T data);
    Task<Operation<TR>?> PutFromJsonAsync<T, TR>(string url, T data);
    Task<Operation<TR>?> DeleteFromJsonAsync<T, TR>(string url);
}