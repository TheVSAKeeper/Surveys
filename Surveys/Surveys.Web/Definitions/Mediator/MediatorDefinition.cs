using Calabonga.AspNetCore.AppDefinitions;
using MediatR;
using Surveys.Web.Definitions.FluentValidating;

namespace Surveys.Web.Definitions.Mediator;

/// <summary>
///     Register Mediator as application definition
/// </summary>
public class MediatorDefinition : AppDefinition
{
    /// <summary>
    ///     Configure services for current application
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());
    }
}