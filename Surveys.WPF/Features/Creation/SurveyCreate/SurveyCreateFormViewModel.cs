using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public class SurveyCreateFormViewModel : ViewModelBase
{
    private ObservableCollection<Anamnesis>? _createdAnamneses;
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
                Answers = questions.Select(question => new AnamnesisAnswer
                    {
                        Question = question,
                        Answers = new List<Answer>()
                    })
                    .ToList()
            });

        CreatedAnamneses = new ObservableCollection<Anamnesis>(anamneses);
    }

    public SurveyCreateFormViewModel(IMediator mediator, IMapper mapper):this()
    {
        SubmitCommand = new SurveyCreateCommand(this, mediator);
        RefreshCommand = new GetAllSurveysCommand(this, mediator);
    }

    public Patient? Patient
    {
        get => _patient;
        set => Set(ref _patient, value);
    }

    public ObservableCollection<Anamnesis>? CreatedAnamneses
    {
        get => _createdAnamneses;
        set => Set(ref _createdAnamneses, value);
    }

    public Survey? CreatedSurvey
    {
        get => _createdSurvey;
        set => Set(ref _createdSurvey, value);
    }

    public ICommand SubmitCommand { get; }
    public ICommand RefreshCommand { get; }
}