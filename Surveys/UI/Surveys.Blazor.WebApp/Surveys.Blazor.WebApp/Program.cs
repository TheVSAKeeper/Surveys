using Blazorise;
using Blazorise.FluentValidation;
using Surveys.Blazor.WebApp.Components;
using Blazorise.Material;
using Blazorise.Icons.Material;
using Serilog;
using Serilog.Events;
using Surveys.Blazor.WebApp.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

AddBlazorise(builder.Services);

builder.Services.AddScoped<AuthorizedHttpClient>();

var app = builder.Build();

// using Serilog request logging
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Surveys.Blazor.WebApp.Client._Imports).Assembly);

app.Run();
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