using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.Navigation.Modal;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.AnamnesesEndpoints.Create;

public class AnamnesesCreateFormViewModel : ViewModelBase, ICallbackViewModel<List<Anamnesis>>
{
    private Action<List<Anamnesis>>? _callback;
    private AnamnesisTemplateDto? _selectedTemplate;
    private List<Anamnesis>? _createdAnamneses;
    private ObservableCollection<AnamnesisTemplateDto>? _anamnesisTemplates;

    public AnamnesesCreateFormViewModel(IMediator mediator, IMapper mapper, CloseModalNavigationService closeNavigationService)
    {
        SubmitCommand = new AnamnesesCreateCommand(this, mediator);
        RefreshCommand = new GetAllAnamnesisTemplatesCommand(this, mediator);
        CancelCommand = new NavigateCommand(closeNavigationService);
    }

    public ICommand SubmitCommand { get; }

    public ICommand RefreshCommand { get; }

    public ICommand CancelCommand { get; }

    public List<Anamnesis>? CreatedAnamneses
    {
        get => _createdAnamneses;
        set
        {
            Set(ref _createdAnamneses, value);
            _callback?.Invoke(_createdAnamneses!);
        }
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

    public void SetCallback(Action<List<Anamnesis>> callback) => _callback ??= callback;
}