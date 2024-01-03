using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Anamnesis.Create;

public class CreateAnamnesisFormViewModel : ViewModelBase
{
    private AnamnesisTemplate? _template;

    private ObservableCollection<AnamnesisAnswer>? _answers;
    private ObservableCollection<AnamnesisTemplate>? _anamnesisTemplates;

    public CreateAnamnesisFormViewModel(IMediator mediator, IMapper mapper)
    {
        //SubmitCommand = new CreateAnamnesisCommand(this, mediator);
    }

    public AnamnesisTemplate? AnamnesisTemplate
    {
        get => _template;
        set => Set(ref _template, value);
    }

    public ObservableCollection<AnamnesisAnswer>? Answers
    {
        get => _answers;
        set => Set(ref _answers, value);
    }   
    
    public ObservableCollection<AnamnesisTemplate>? AnamnesisTemplates
    {
        get => _anamnesisTemplates;
        set => Set(ref _anamnesisTemplates, value);
    }
}