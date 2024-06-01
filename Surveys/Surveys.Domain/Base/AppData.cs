namespace Surveys.Domain.Base;

/// <summary>
///     Static data container
/// </summary>
public static partial class AppData
{
    /// <summary>
    ///     Current service name
    /// </summary>
    public const string ServiceName = "Microservice Template with OpenIddict";

    /// <summary>
    ///     Nimble Framework Microservice Template with integrated OpenIddict
    ///     for OpenID Connect server and Token Validation
    /// </summary>
    public const string ServiceDescription = "Nimble Framework Microservice Template with integrated OpenIddict for OpenID Connect server and Token Validation";

    /// <summary>
    ///     Default policy name for CORS
    /// </summary>
    public const string PolicyCorsName = "CorsPolicy";

    /// <summary>
    ///     Default policy name for API
    /// </summary>
    public const string PolicyDefaultName = "DefaultPolicy";

    /// <summary>
    ///     "SystemAdministrator"
    /// </summary>
    public const string SystemAdministratorRoleName = "Administrator";

    /// <summary>
    ///     "Doctor"
    /// </summary>
    public const string DoctorRoleName = "Doctor";

    /// <summary>
    ///     "Nurse"
    /// </summary>
    public const string NurseRoleName = "Nurse";

    /// <summary>
    ///     Roles
    /// </summary>
    public static IEnumerable<string> Roles
    {
        get
        {
            yield return SystemAdministratorRoleName;
            yield return DoctorRoleName;
            yield return NurseRoleName;
        }
    }
}