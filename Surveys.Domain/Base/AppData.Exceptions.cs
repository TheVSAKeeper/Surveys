namespace Surveys.Domain.Base;

public static partial class AppData
{
    public static class Exceptions
    {
        public static string ThrownException => "An exception was thrown";

        public static string TypeConverterException => "Invalid cast exception";

        public static string UserNotFoundException => "User not registered in the system";

        public static string NotFoundException => "Object not found";

        public static string UnauthorizedException => "Unauthorized access denied";

        public static string ArgumentNullException => "Argument null exception";

        public static string FileAlreadyExists => "File already exists";

        public static string EntityValidationException => "Some errors occurred while checking the entity";

        public static string InvalidOperationException => "Invalid operation exception was thrown";

        public static string ArgumentOutOfRangeException => "Argument out of range";
    }
}