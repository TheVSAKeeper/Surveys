using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Creation.AnamnesesCreate;

public class AnamnesesCreateFormViewModel : ViewModelBase
{
    private AnamnesisTemplateDto? _selectedTemplate;
    private List<Anamnesis>? _createdAnamneses;
    private ObservableCollection<AnamnesisTemplateDto>? _anamnesisTemplates;

    public AnamnesesCreateFormViewModel()
    {
        IEnumerable<Question> questions = Enumerable.Range(0, 10)
            .Select(x => new Question
            {
                Content = $"Question {x}"
            });

        IEnumerable<AnamnesisTemplateDto> anamnesisTemplates = Enumerable.Range(0, 10)
            .Select(x => new AnamnesisTemplateDto
            {
                Name = $"Template {x}",
                Questions = new ObservableCollection<Question>(questions)
            });

        AnamnesisTemplates = new ObservableCollection<AnamnesisTemplateDto>(anamnesisTemplates);
    }

    public AnamnesesCreateFormViewModel(IMediator mediator, IMapper mapper)
    {
        SubmitCommand = new AnamnesesCreateCommand(this, mediator);
        RefreshCommand = new GetAllAnamnesisTemplatesCommand(this, mediator);
    }

    public Survey? Survey { get; set; }
    public ICommand SubmitCommand { get; }

    public ICommand RefreshCommand { get; }

    public List<Anamnesis>? CreatedAnamneses
    {
        get => _createdAnamneses;
        set => Set(ref _createdAnamneses, value);
    }

    public AnamnesisTemplateDto? SelectedTemplate
    {
        get => _selectedTemplate;
        set => Set(ref _selectedTemplate, value);
    }

    public ObservableCollection<AnamnesisTemplateDto>? AnamnesisTemplates
    {
        get => _anamnesisTemplates;
        set => Set(ref _anamnesisTemplates, value);
    }
}