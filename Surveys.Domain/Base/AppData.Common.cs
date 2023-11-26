namespace Surveys.Domain.Base;

public static partial class AppData
{
    public const string ServiceName = "Patient Surveys";

    public const string ServiceDescription = "";

    public const string PolicyName = "CorsPolicy";

    public const string SystemAdministratorRoleName = "Administrator";
    public const string DoctorRoleName = "Doctor";
    public const string NurseRoleName = "Nurse";

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