using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Surveys.Blazor.Domain;

namespace Surveys.Blazor.Services;

public class AuthorizedHttpClient(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : IAuthorizedHttpClient
{
    private const string ApiUrl = "https://localhost:10001/api/";
    private const string AuthorizationShame = "Bearer";

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        HttpRequestMessage request = new(HttpMethod.Get, ApiUrl + url);
        await AddAuthorizationHeaderAsync(request);
        return await httpClient.SendAsync(request);
    }

    public async Task<Operation<T>?> GetFromJsonAsync<T>(string url)
    {
        await AddAuthorizationHeaderAsync(httpClient);
        return await httpClient.GetFromJsonAsync<Operation<T>>(ApiUrl + url);
    }

    public async Task<Operation<PagedListResult<T>>?> GetPagedAsync<T>(string url, int pageIndex = 0, int pageSize = 10, string search = "")
    {
        await AddAuthorizationHeaderAsync(httpClient);
        return await httpClient.GetFromJsonAsync<Operation<PagedListResult<T>>>($"{ApiUrl}{url}/paged/{pageIndex}?pageSize={pageSize}&search={search}");
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
    {
        HttpRequestMessage request = new(HttpMethod.Post, ApiUrl + url)
        {
            Content = JsonContent.Create(data)
        };

        await AddAuthorizationHeaderAsync(request);
        return await httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string url, T data)
    {
        HttpRequestMessage request = new(HttpMethod.Put, ApiUrl + url)
        {
            Content = JsonContent.Create(data)
        };

        await AddAuthorizationHeaderAsync(request);
        return await httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        HttpRequestMessage request = new(HttpMethod.Delete, ApiUrl + url);
        await AddAuthorizationHeaderAsync(request);
        return await httpClient.SendAsync(request);
    }

    public async Task<Operation<TR>?> PostFromJsonAsync<T, TR>(string url, T data)
    {
        await AddAuthorizationHeaderAsync(httpClient);
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(ApiUrl + url, data);
        return await response.Content.ReadFromJsonAsync<Operation<TR>>();
    }

    public async Task<Operation<TR>?> PutFromJsonAsync<T, TR>(string url, T data)
    {
        await AddAuthorizationHeaderAsync(httpClient);
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(ApiUrl + url, data);
        return await response.Content.ReadFromJsonAsync<Operation<TR>>();
    }

    public async Task<Operation<TR>?> DeleteFromJsonAsync<T, TR>(string url)
    {
        await AddAuthorizationHeaderAsync(httpClient);
        HttpResponseMessage response = await httpClient.DeleteAsync(ApiUrl + url);
        return await response.Content.ReadFromJsonAsync<Operation<TR>>();
    }

    private async Task AddAuthorizationHeaderAsync(HttpRequestMessage request)
    {
        AccessTokenResult tokenResult = await accessTokenProvider.RequestAccessToken();

        if (tokenResult.TryGetToken(out AccessToken? token))
            request.Headers.Authorization = new AuthenticationHeaderValue(AuthorizationShame, token.Value);
    }

    private async Task AddAuthorizationHeaderAsync(HttpClient client)
    {
        AccessTokenResult tokenResult = await accessTokenProvider.RequestAccessToken();

        if (tokenResult.TryGetToken(out AccessToken? token))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationShame, token.Value);
    }
}