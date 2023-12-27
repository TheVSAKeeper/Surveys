using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Definitions.Initialization;

namespace Surveys.WPF;

public partial class App : Application
{
    private static IHost? _host;
    public static bool IsDesignTime { get; private set; } = true;

    public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    public static IServiceProvider Services => Host.Services;

    internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
        .AddDefinitions(host, typeof(Program));

    protected override async void OnStartup(StartupEventArgs e)
    {
        await Host.StartAsync();

        ApplicationInitializer applicationInitializer = Services.GetRequiredService<ApplicationInitializer>();
        await applicationInitializer.Initialize();

        IsDesignTime = false;

        base.OnStartup(e);
        Host.UseDefinitions();

        MainWindow = Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using IHost host = Host;

        base.OnExit(e);
        await host.StopAsync();
    }
}