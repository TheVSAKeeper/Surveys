using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Surveys.Infrastructure;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Pages.Login;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Definitions.Initialization;

public class ApplicationInitializer : AppDefinition
{
    public override int OrderIndex => 100;

    public override async Task ConfigureApplication(IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();

        ILogger<ApplicationInitializer> logger = host.Services.GetRequiredService<ILogger<ApplicationInitializer>>();

        try
        {
            await using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
                                                       ?? throw new InvalidOperationException($"{typeof(ApplicationDbContext)} dont registered");

            //await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            IEnumerable<string> pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
                await context.Database.MigrateAsync();

            Endpoints.AuthenticationEndpoints.AuthenticationStore authenticationStore = scope.ServiceProvider.GetRequiredService<Endpoints.AuthenticationEndpoints.AuthenticationStore>();
            await authenticationStore.Initialize();

            if (authenticationStore.IsLoggedIn)
            {
                NavigationService<HomeViewModel> homeNavigationService = scope.ServiceProvider.GetRequiredService<NavigationService<HomeViewModel>>();
                homeNavigationService.Navigate();
            }
            else
            {
                NavigationService<LoginViewModel> loginNavigationService = scope.ServiceProvider.GetRequiredService<NavigationService<LoginViewModel>>();
                loginNavigationService.Navigate();
            }
        }
        catch (Exception exception)
        {
            logger.LogCritical(exception, "Unhandled exception");
            MessageBox.Show("Failed to load application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}