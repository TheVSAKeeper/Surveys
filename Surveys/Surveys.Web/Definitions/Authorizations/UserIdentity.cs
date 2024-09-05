using System.Security.Claims;
using System.Security.Principal;
using Calabonga.Microservices.Core.Exceptions;

namespace Surveys.Web.Definitions.Authorizations;

/// <summary>
///     Identity helper for Requests operations
/// </summary>
public sealed class UserIdentity
{
    private static readonly Lazy<UserIdentity> Lazy = new(() => new UserIdentity());

    private UserIdentity()
    {
    }

    public static UserIdentity Instance => Lazy.Value;

    public IIdentity? User
    {
        get
        {
            if (IsInitialized)
            {
                return ContextAccessor.HttpContext!.User.Identity != null
                       && ContextAccessor.HttpContext != null
                       && ContextAccessor.HttpContext.User.Identity.IsAuthenticated
                    ? ContextAccessor.HttpContext.User.Identity
                    : null;
            }

            throw new MicroserviceArgumentNullException($"{nameof(UserIdentity)} has not been initialized. Please use {nameof(UserIdentity)}.Instance.Configure(...) in Configure Application method in Startup.cs");
        }
    }

    public IEnumerable<Claim> Claims
        => User != null
            ? ContextAccessor.HttpContext!.User.Claims
            : Enumerable.Empty<Claim>();

    private static IHttpContextAccessor ContextAccessor { get; set; } = null!;

    private bool IsInitialized { get; set; }

    public void Configure(IHttpContextAccessor httpContextAccessor)
    {
        ContextAccessor = httpContextAccessor ?? throw new MicroserviceArgumentNullException(nameof(IHttpContextAccessor));
        IsInitialized = true;
    }
}
