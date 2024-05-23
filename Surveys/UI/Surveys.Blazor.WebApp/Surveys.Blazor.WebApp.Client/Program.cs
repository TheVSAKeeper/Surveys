using Blazorise.FluentValidation;
using Blazorise.Icons.Material;
using Blazorise.Material;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Surveys.Blazor.WebApp.Client.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

AddBlazorise(builder.Services);

builder.Services.AddScoped<AuthorizedHttpClient>();

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
return;

void AddBlazorise(IServiceCollection services)
{
    services
        .AddBlazorise()
        .AddBlazoriseFluentValidation();

    services
        .AddMaterialProviders()
        .AddMaterialIcons();
}