using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Debug()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .CreateLogger();

            IHost host = Host.CreateDefaultBuilder(args)
                             .UseSerilog()
                             .ConfigureServices((host, serviceCollection) => serviceCollection.AddDefinitions(host, typeof(Program)))
                             .Build();

            App app = new();

            app.InitializeComponent();
            app.Run();
        }
        catch (Exception ex)
        {
            string type = ex.GetType().Name;

            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
                throw;

            Log.Fatal(ex, "Unhandled exception");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host
                                                                   .CreateDefaultBuilder(args)
                                                                   .ConfigureServices(App.ConfigureServices);
}