using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Creation.Create;

public class GetAllAnamnesisTemplatesCommand(AnamnesesCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<List<AnamnesisTemplate>> result = await mediator.Send(new GetAllAnamnesisTemplatesRequest());

        if (result.Ok)
            viewModel.AnamnesisTemplates = new ObservableCollection<AnamnesisTemplate>(result.Result!);
    }
}

public record GetAllAnamnesisTemplatesRequest : IRequest<OperationResult<List<AnamnesisTemplate>>>;

public class GetAllAnamnesisTemplatesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllAnamnesisTemplatesRequest, OperationResult<List<AnamnesisTemplate>>>
{
    public async Task<OperationResult<List<AnamnesisTemplate>>> Handle(GetAllAnamnesisTemplatesRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<AnamnesisTemplate>> operation = OperationResult.CreateResult<List<AnamnesisTemplate>>();

        IList<AnamnesisTemplate> templates = await unitOfWork.GetRepository<AnamnesisTemplate>()
            .GetAllAsync(disableTracking: true,
                include: i => i.Include(template => template.Questions));

        return OperationResult.CreateResult(templates.ToList());
    }
}

public class AnamnesesCreateFormViewModel : ViewModelBase
{
    private AnamnesisTemplate? _template;

    private ObservableCollection<AnamnesisAnswer>? _answers;
    private ObservableCollection<AnamnesisTemplate>? _anamnesisTemplates;

    public AnamnesesCreateFormViewModel()
    {
        IEnumerable<Question> questions = Enumerable.Range(0, 10)
            .Select(x => new Question
            {
                Content = $"Question {x}"
            });

        IEnumerable<AnamnesisTemplate> anamnesisTemplates = Enumerable.Range(0, 10)
            .Select(x => new AnamnesisTemplate
            {
                Name = $"Template {x}",
                Questions = new ObservableCollection<Question>(questions)
            });

        AnamnesisTemplates = new ObservableCollection<AnamnesisTemplate>(anamnesisTemplates);
    }

    public AnamnesesCreateFormViewModel(IMediator mediator, IMapper mapper)
    {
        SubmitCommand = new AnamnesesCreateCommand(this, mediator);
        RefreshCommand = new GetAllAnamnesisTemplatesCommand(this, mediator);
    }

    public ICommand SubmitCommand { get; }
    public ICommand RefreshCommand { get; }

    public AnamnesisTemplate? AnamnesisTemplate
    {
        get => _template;
        set { Set(ref _template, value); }
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