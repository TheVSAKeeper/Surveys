using System.Security.Claims;
using Calabonga.Microservices.Core;

namespace Surveys.Web.Application.Messaging.ProfileMessages.Queries;

public sealed class GetRoles
{
    public class Handler(ILogger<Handler> logger, IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<Request, string>
    {
        public Task<string> Handle(Request request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal user = httpContextAccessor.HttpContext!.User;
            IEnumerable<Claim> claims = user.FindAll(claim => claim.Type == ClaimTypes.Role);
            List<string>? roles = ClaimsHelper.GetValues<string>(new ClaimsIdentity(claims), ClaimTypes.Role);

            string message = $"Current user ({user.Identity!.Name}) have following roles: {string.Join('|', roles)}";
            logger.LogInformation(message);
            return Task.FromResult(message);
        }
    }

    public record Request : IRequest<string>;
}
