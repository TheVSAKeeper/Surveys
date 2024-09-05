using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Surveys.Web.Definitions.ETagGenerator;

/// <summary>
///     ETag Generator
/// </summary>
public class ETagGeneratorDefinition : AppDefinition
{
    public override bool Enabled => true;

    /// <summary>
    ///     Configure application for current application
    /// </summary>
    /// <param name="app"></param>
    public override void ConfigureApplication(WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            HttpResponse response = context.Response;
            Stream originalStream = response.Body;

            await using MemoryStream memoryStream = new();
            response.Body = memoryStream;

            await next(context);

            if (IsEtagSupported(response))
            {
                string checksum = CalculateChecksum(memoryStream);

                response.Headers[HeaderNames.ETag] = checksum;

                if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out StringValues etag) && checksum == etag)
                {
                    response.StatusCode = StatusCodes.Status304NotModified;
                    return;
                }
            }

            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(originalStream);
        });
    }

    private static bool IsEtagSupported(HttpResponse response)
    {
        if (response.StatusCode != StatusCodes.Status200OK)
        {
            return false;
        }

        // The 100kb length limit is not based in science. Feel free to change
        if (response.Body.Length > 100 * 1024)
        {
            return false;
        }

        return !response.Headers.ContainsKey(HeaderNames.ETag);
    }

    private static string CalculateChecksum(MemoryStream memoryStream)
    {
        using SHA1 algorithm = SHA1.Create();
        memoryStream.Position = 0;
        byte[] bytes = algorithm.ComputeHash(memoryStream);
        return $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";
    }
}
