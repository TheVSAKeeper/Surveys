namespace Surveys.Domain.Exceptions;

public class SurveysDatabaseSaveException : Exception
{
    public SurveysDatabaseSaveException(string entityName)
        : base($"Saving data error for entity name {entityName}")
    {
    }

    public SurveysDatabaseSaveException(string entityName, Exception? exception)
        : base($"Saving data error for entity name {entityName}", exception)
    {
    }
}