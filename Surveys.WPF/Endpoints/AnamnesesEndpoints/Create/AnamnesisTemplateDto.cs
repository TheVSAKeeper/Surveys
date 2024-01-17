using System.Collections.ObjectModel;
using Surveys.Domain;

namespace Surveys.WPF.Endpoints.AnamnesesEndpoints.Create;

public class AnamnesisTemplateDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required ObservableCollection<Question> Questions { get; set; }
    public bool IsSelected { get; set; }

    public required int SortIndex { get; set; }
}