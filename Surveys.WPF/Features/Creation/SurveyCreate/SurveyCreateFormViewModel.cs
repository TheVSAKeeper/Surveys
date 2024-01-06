using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Features.Creation.AnamnesesCreate;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public class SurveyCreateFormViewModel : ViewModelBase
{
    private ObservableCollection<Anamnesis>? _anamneses;
    private Patient? _patient;
    private Survey? _createdSurvey;

    public SurveyCreateFormViewModel()
    {
        Patient = new Patient
        {
            LastName = "LastName",
            FirstName = "FirstName",
            Patronymic = "Patronymic",
            Gender = Gender.Male,
            BirthDate = new DateOnly()
        };

        IEnumerable<Question> questions = Enumerable.Range(0, 10)
            .Select(x => new Question
            {
                Content = $"Question {x}"
            });

        AnamnesisTemplate[] anamnesisTemplates = Enumerable.Range(0, 5)
            .Select(x => new AnamnesisTemplate
            {
                Name = $"Template {x}",
                Questions = new ObservableCollection<Question>(questions)
            })
            .ToArray();

        IEnumerable<Anamnesis> anamneses = Enumerable.Range(0, 5)
            .Select(x => new Anamnesis
            {
                AnamnesisTemplate = anamnesisTemplates[x],
                AnamnesisAnswers = questions.Select(question => new AnamnesisAnswer
                    {
                        Question = question,
                        Answers = new List<Answer>()
                    })
                    .ToList()
            });

        //CreatedAnamneses = new ObservableCollection<Anamnesis>(anamneses);

        CreatedSurvey = new Survey
        {
            Patient = Patient,
            CreatedAt = DateTime.UtcNow
        };
    }

    public SurveyCreateFormViewModel(IMediator mediator, IMapper mapper)
    {
        SubmitCommand = new SurveyCreateCommand(this, mediator);
        AnamnesesCreateFormViewModel = new AnamnesesCreateFormViewModel(mediator, mapper);

        AnamnesesCreateFormViewModel.AnamnesesCreated += OnAnamnesesCreated;
    }

    public Patient? Patient
    {
        get => _patient;
        set => Set(ref _patient, value);
    }

    public Survey? CreatedSurvey
    {
        get => _createdSurvey;
        set => Set(ref _createdSurvey, value);
    }

    public ObservableCollection<Anamnesis>? Anamneses
    {
        get => _anamneses;
        set => Set(ref _anamneses, value);
    }

    public ICommand SubmitCommand { get; }

    public AnamnesesCreateFormViewModel AnamnesesCreateFormViewModel { get; set; }

    private void OnAnamnesesCreated(List<Anamnesis> list)
    {
        List<Anamnesis> old;

        if (Anamneses == null) { old = list; }
        else
        {
            old = Anamneses.ToList();
            old.AddRange(list);
        }

        Anamneses = new ObservableCollection<Anamnesis>(old);
    }

    public override void Dispose()
    {
        base.Dispose();
        AnamnesesCreateFormViewModel.AnamnesesCreated -= OnAnamnesesCreated;
    }
}