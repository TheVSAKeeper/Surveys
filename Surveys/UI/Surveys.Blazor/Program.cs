using Blazorise;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazorise.Material;
using Blazorise.Icons.Material;
using Surveys.Blazor;
using Surveys.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AddBlazorise(builder.Services);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("Surveys.ServerAPI")
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    // .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
    ;

builder.Services.AddScoped<AuthorizedHttpClient>();

// Supply HttpClient instances that include access tokens when making requests to the server project.
builder.Services.AddScoped(provider =>
{
    IHttpClientFactory factory = provider.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("Surveys.ServerAPI");
});


builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.ClientId = "blazor-client";
    options.ProviderOptions.Authority = "https://localhost:10001/";
    
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.ResponseMode = "query";
  
    options.ProviderOptions.DefaultScopes.Add("roles");
    options.UserOptions.RoleClaim = "role";
});


await builder.Build().RunAsync();

void AddBlazorise(IServiceCollection services)
{
    services
        .AddBlazorise();

    services
        .AddMaterialProviders()
        .AddMaterialIcons();
}