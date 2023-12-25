namespace Surveys.WPF.Exceptions;

public class SurveysAccessDeniedException : Exception
{
    public SurveysAccessDeniedException() : base("Access denied")
    {
    }
}