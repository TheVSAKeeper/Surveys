namespace Surveys.Web.Definitions.Cors;

/// <summary>
///     Cors configurations
/// </summary>
public class CorsDefinition : AppDefinition
{
    /// <summary>
    ///     Configure services for current application
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        string[]? origins = builder.Configuration.GetSection("Cors").GetSection("Origins").Value?.Split(',');

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(AppData.PolicyCorsName, policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();

                if (origins is not { Length: > 0 })
                {
                    return;
                }

                if (origins.Contains("*"))
                {
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.SetIsOriginAllowed(_ => true);
                    policyBuilder.AllowCredentials();
                }
                else
                {
                    foreach (string origin in origins)
                    {
                        policyBuilder.WithOrigins(origin);
                    }
                }
            });
        });
    }
}
