using System.Collections.ObjectModel;
using Calabonga.OperationResults;
using MediatR;
using System.Windows;
using Surveys.Domain;
using Surveys.WPF.Features.Authentication.Update;
using Surveys.WPF.Features.Authentication;
using Surveys.WPF.Shared.Commands;
using Surveys.WPF.Shared.ViewModels;
using AutoMapper;
using Calabonga.UnitOfWork;
using Surveys.Infrastructure;
using Surveys.WPF.Exceptions;

namespace Surveys.WPF.Features.Anamnesis.Create;
public class AnamnesisCreateCommand(CreateAnamnesisFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        OperationResult<Anamnesis> result = await mediator.Send(new AnamnesisCreateRequest(viewModel.AnamnesisTemplate));

        if (result.Ok)
            MessageBox.Show("Пользователь обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
        else
            MessageBox.Show("Ошибка обновления пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }Ы
}

public record AnamnesisCreateRequest(ApplicationUserUpdateDto Model) : IRequest<OperationResult<Anamnesis>>;

public class AnamnesisCreateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AnamnesisCreateRequest, OperationResult<Anamnesis>>
{
    public async Task<OperationResult<Anamnesis>> Handle(AnamnesisCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<Guid> operation = OperationResult.CreateResult<Guid>();

        return operation;
    }


public class CreateAnamnesisFormViewModel : ViewModelBase
{
    private AnamnesisTemplate? _template;

    private ObservableCollection<AnamnesisAnswer>? _answers;
    private ObservableCollection<AnamnesisTemplate>? _anamnesisTemplates;

    public CreateAnamnesisFormViewModel()
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
        SubmitCommand = new AnamnesisCreateCommand(this, mediator);
    }

    public AnamnesisTemplate? AnamnesisTemplate
    {
        get => _template;
        set
        {
            Set(ref _template, value);
            OnPropertyChanged(nameof(IsTemplateSeleted));
        }
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

    public bool IsTemplateSeleted => AnamnesisTemplate is not null;
}