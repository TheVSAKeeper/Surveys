namespace Surveys.Domain.Base;

public static partial class AppData
{
    public const string ServiceName = "Patient Surveys";

    public const string ServiceDescription = "";

    /// <summary>
    /// Default policy name for CORS
    /// </summary>
    public const string PolicyCorsName = "CorsPolicy";

    /// <summary>
    /// Default policy name for API
    /// </summary>
    public const string PolicyDefaultName = "DefaultPolicy";

    public const string SystemAdministratorRoleName = "Administrator";
    public const string DoctorRoleName = "Doctor";
    public const string NurseRoleName = "Nurse";
    public const string ManagerRoleName = "Manager";


    public static IEnumerable<string> Roles
    {
        get
        {
            yield return SystemAdministratorRoleName;
            yield return ManagerRoleName;
            yield return DoctorRoleName;
            yield return NurseRoleName;
        }
    }
}