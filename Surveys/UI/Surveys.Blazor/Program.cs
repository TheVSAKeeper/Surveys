using Blazorise;
using Blazorise.FluentValidation;
using Blazorise.Icons.FontAwesome;
using Blazorise.Icons.Material;
using Blazorise.Material;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Events;
using Surveys.Blazor;
using Surveys.Blazor.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// configure logger (Serilog)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

AddBlazorise(builder.Services);

builder.Services.AddValidatorsFromAssembly(typeof(App).Assembly);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("Surveys.ServerAPI")
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

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
return;

void AddBlazorise(IServiceCollection services)
{
    services
        .AddBlazorise()
        .AddBlazoriseFluentValidation();

    services
        .AddMaterialProviders()
        .AddMaterialIcons()
        .AddFontAwesomeIcons();
}