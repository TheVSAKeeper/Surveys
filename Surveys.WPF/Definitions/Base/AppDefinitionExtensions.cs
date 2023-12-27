using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Surveys.WPF.Definitions.Base;

public static class AppDefinitionExtensions
{
    private static bool Predicate(Type type) => type is { IsAbstract: false, IsInterface: false } && typeof(AppDefinition).IsAssignableFrom(type);

    public static IHostBuilder AddDefinitions(this IHostBuilder builder, params Type[] entryPointsAssembly)
    {
        return builder.ConfigureServices((context, collection) => collection.AddDefinitions(context, entryPointsAssembly));
    }

    public static void AddDefinitions(this IServiceCollection services, HostBuilderContext context, params Type[] entryPointsAssembly)
    {
        ILogger<AppDefinition> logger = services.BuildServiceProvider().GetRequiredService<ILogger<AppDefinition>>();
        List<IAppDefinition> addedDefinitions = [];

        foreach (Type type in entryPointsAssembly)
        {
            List<IAppDefinition> foundedDefinitions = type.Assembly.ExportedTypes
                .Where(Predicate)
                .Select(Activator.CreateInstance)
                .Cast<IAppDefinition>()
                .OrderByDescending(definition => definition.OrderIndex)
                .ToList();

            List<IAppDefinition> enabledDefinitions = foundedDefinitions.Where(definition => definition.Enabled).ToList();

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("[AppDefinitions] Founded: {AppDefinitionsCountTotal}. Enabled: {AppDefinitionsCountEnabled}",
                    foundedDefinitions.Count, enabledDefinitions.Count);

                logger.LogDebug("[AppDefinitions] Registered [{Total}]",
                    string.Join(", ", enabledDefinitions.Select(definition => definition.GetType().Name)));
            }

            addedDefinitions.AddRange(enabledDefinitions);
        }

        addedDefinitions.ForEach(definition => definition.ConfigureServices(services, context));

        services.AddSingleton((IReadOnlyCollection<IAppDefinition>)addedDefinitions);
    }

    public static async Task UseDefinitions(this IHost host)
    {
        ILogger<AppDefinition> logger = host.Services.GetRequiredService<ILogger<AppDefinition>>();

        List<IAppDefinition> definitions = host.Services.GetRequiredService<IReadOnlyCollection<IAppDefinition>>()
            .Where(definition => definition.Enabled)
            .OrderByDescending(definition => definition.OrderIndex)
            .ToList();

        foreach (IAppDefinition definition in definitions)
            await definition.ConfigureApplication(host);

        logger.LogDebug("[AppDefinitions] Total application definitions configured {Count}", definitions.Count);

        logger.LogDebug("[AppDefinitions] Used [{Total}]",
            string.Join(", ", definitions.Select(definition => definition.GetType().Name)));
    }
}