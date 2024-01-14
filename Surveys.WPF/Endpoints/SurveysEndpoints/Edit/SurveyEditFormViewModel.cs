using System.Collections.ObjectModel;
using System.Windows.Input;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.ViewModels;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

public class SurveyEditFormViewModel : ViewModelBase
{
    private SurveyEditDto? _survey;

    public SurveyEditFormViewModel()
    {
        Patient patient = new()
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
                        Answers = new List<Answer> { new() { Content = "Answer" } }
                    })
                    .ToList()
            });

        //CreatedAnamneses = new ObservableCollection<Anamnesis>(anamneses);

        Survey = new SurveyEditDto
        {
            Patient = patient,
            CreatedAt = DateTime.UtcNow,
            Id = default,
            Complaint = "null",
            IsComplete = false,
            CreatedBy = "null",
            Anamneses = anamneses.ToList()
        };
    }

    public SurveyEditFormViewModel(
        IMediator mediator,
        Guid surveyId
    )
    {
        LoadCommand = new SurveyLoadCommand(this, mediator, surveyId);
        SubmitCommand = new SurveyUpdateCommand(this, mediator);
    }

    public SurveyEditDto? Survey
    {
        get => _survey;
        set => Set(ref _survey, value);
    }

    public ICommand SubmitCommand { get; }
    public ICommand LoadCommand { get; }
}