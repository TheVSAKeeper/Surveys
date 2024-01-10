namespace Surveys.Domain.Exceptions;

public class SurveysArgumentNullException : ArgumentNullException
{
    public SurveysArgumentNullException(string argumentName)
        : base($"The {argumentName} is NULL")
    {
    }

    public SurveysArgumentNullException(string argumentName, Exception? exception)
        : base($"The {argumentName} is NULL", exception)
    {
    }
}