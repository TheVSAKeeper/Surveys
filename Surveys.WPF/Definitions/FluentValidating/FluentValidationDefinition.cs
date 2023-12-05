using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;

namespace Surveys.WPF.Definitions.FluentValidating;

public class FluentValidationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}