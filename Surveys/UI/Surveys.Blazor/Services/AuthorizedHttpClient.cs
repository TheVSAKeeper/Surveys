using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Surveys.Blazor.Services;

public class AuthorizedHttpClient(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
{
    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        HttpRequestMessage request = new(HttpMethod.Get, requestUri);
        AccessTokenResult tokenResult = await accessTokenProvider.RequestAccessToken();

        if (tokenResult.TryGetToken(out AccessToken? token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        return await httpClient.SendAsync(request);
    }

    public async Task<T?> GetFromJsonAsync<T>(string url)
    {
        AccessTokenResult tokenResult = await accessTokenProvider.RequestAccessToken();

        if (tokenResult.TryGetToken(out AccessToken? token))
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        T? response = await httpClient.GetFromJsonAsync<T>(url);
        return response;
    }
}