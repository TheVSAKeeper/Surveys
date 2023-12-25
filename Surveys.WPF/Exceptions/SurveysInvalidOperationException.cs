namespace Surveys.WPF.Exceptions;

public class SurveysInvalidOperationException : InvalidOperationException
{
    public SurveysInvalidOperationException(string operationName, string reason)
        : base($"The {operationName} cannot be executed because {reason}")
    {
    }

    public SurveysInvalidOperationException(string operationName, string reason, Exception? exception)
        : base($"The {operationName} cannot be executed because {reason}", exception)
    {
    }
}