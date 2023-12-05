using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Surveys.WPF.Definitions.Base;

public static class AppDefinitionExtensions
{
    private static bool Predicate(Type type) => type is { IsAbstract: false, IsInterface: false } && typeof(AppDefinition).IsAssignableFrom(type);

    public static void AddDefinitions(this IServiceCollection source, HostBuilderContext context, params Type[] entryPointsAssembly)
    {
        ILogger<AppDefinition> logger = source.BuildServiceProvider().GetRequiredService<ILogger<AppDefinition>>();
        List<IAppDefinition> addedDefinitions = [];

        foreach (Type type in entryPointsAssembly)
        {
            List<IAppDefinition> foundedDefinitions = type.Assembly.ExportedTypes
                                                          .Where(x => Predicate(type))
                                                          .Select(Activator.CreateInstance)
                                                          .Cast<IAppDefinition>()
                                                          .ToList();

            List<IAppDefinition> enabledDefinitions = foundedDefinitions.Where(x => x.Enabled).ToList();

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("[AppDefinitions] Founded: {AppDefinitionsCountTotal}. Enabled: {AppDefinitionsCountEnabled}",
                    foundedDefinitions.Count,
                    enabledDefinitions.Count);

                logger.LogDebug("[AppDefinitions] Registered [{Total}]",
                    string.Join(", ", enabledDefinitions.Select(x => x.GetType().Name)));
            }

            addedDefinitions.AddRange(enabledDefinitions);
        }

        addedDefinitions.ForEach(app => app.ConfigureServices(source, context));
        source.AddSingleton((IReadOnlyCollection<IAppDefinition>)addedDefinitions);
    }
}