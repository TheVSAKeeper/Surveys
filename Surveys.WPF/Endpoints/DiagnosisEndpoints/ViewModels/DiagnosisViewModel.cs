namespace Surveys.WPF.Endpoints.DiagnosisEndpoints.ViewModels;

public class DiagnosisViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public override string ToString() => $"{Id}: {Name}({Description})";
}