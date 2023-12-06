namespace Surveys.Domain.Exceptions;

public class SurveysArgumentNullException(string executeName) : ArgumentNullException(executeName);