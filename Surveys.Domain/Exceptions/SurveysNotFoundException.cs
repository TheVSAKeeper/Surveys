namespace Surveys.Domain.Exceptions;

public class SurveysNotFoundException : Exception
{
    public SurveysNotFoundException(string entityName, string id)
        : base($"Item {entityName} with {id} not found")
    {
    }

    public SurveysNotFoundException(string entityName, string id, Exception? exception)
        : base($"Item {entityName} with {id} not found", exception)
    {
    }
}