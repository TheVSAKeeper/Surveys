namespace Surveys.Blazor.Services;

public interface IAuthorizedHttpClient
{
    Task<HttpResponseMessage> GetAsync(string url);
    Task<T?> GetFromJsonAsync<T>(string url);
    Task<HttpResponseMessage> PostAsync<T>(string url, T data);
    Task<HttpResponseMessage> PutAsync<T>(string url, T data);
    Task<HttpResponseMessage> DeleteAsync(string url);
}