using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

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

    public async Task<T?> GetFromJsonAsync<T>(string url)
    {
        await AddAuthorizationHeaderAsync(httpClient);
        return await httpClient.GetFromJsonAsync<T>(ApiUrl + url);
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