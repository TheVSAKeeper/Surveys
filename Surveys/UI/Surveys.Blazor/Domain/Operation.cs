namespace Surveys.Blazor.Domain;

public record Operation<T>
{
    public T Result { get; set; }
    public bool Ok { get; set; }
}