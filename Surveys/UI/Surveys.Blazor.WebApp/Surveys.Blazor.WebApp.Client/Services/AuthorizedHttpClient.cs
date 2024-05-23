using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Surveys.Blazor.WebApp.Client.Services;

internal class AuthorizedHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IAccessTokenProvider _accessTokenProvider;

    public AuthorizedHttpClient(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
    {
        _httpClient = httpClient;
        _accessTokenProvider = accessTokenProvider;
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        HttpRequestMessage request = new(HttpMethod.Get, requestUri);
        AccessTokenResult tokenResult = await _accessTokenProvider.RequestAccessToken();

        if (tokenResult.TryGetToken(out AccessToken? token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        return await _httpClient.SendAsync(request);
    }
}