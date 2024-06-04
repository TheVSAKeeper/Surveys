using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Surveys.Blazor.Endpoints.QuestionEndpoints.ViewModels;

namespace Surveys.Blazor.Endpoints.QuestionEndpoints.Components;

public partial class QuestionCard
{
    [Parameter] public required QuestionViewModel Question { get; set; }
    [Parameter] public EventCallback<AnswerSavedEventArgs> OnAnswerSaved { get; set; }

    private string SelectedOption { get; set; } = string.Empty;
    private string AnswerText { get; set; } = string.Empty;
    private decimal AnswerNumber { get; set; }
    private DateTime AnswerDate { get; set; }

    private void OnAnswerChanged(FocusEventArgs focusEventArgs)
    {
        string answer = GetAnswer();
        OnAnswerSaved.InvokeAsync(new AnswerSavedEventArgs(answer, Question.Id));
    }

    private string GetAnswer()
    {
        return Question.Type switch
        {
            QuestionType.SingleChoice or QuestionType.MultipleChoice => SelectedOption,
            QuestionType.Text => AnswerText,
            QuestionType.Number => AnswerNumber.ToString(CultureInfo.InvariantCulture),
            QuestionType.Date => AnswerDate.ToString(CultureInfo.InvariantCulture),
            var _ => string.Empty
        };
    }

    protected override void OnParametersSet()
    {
        /*if (Question.Answers?.Count > 0)
        {
            switch (Question.Type)
            {
                case QuestionType.SingleChoice:
                case QuestionType.MultipleChoice:
                    SelectedOption = Question.Answers.First().Id;
                    break;

                case QuestionType.Text:
                    AnswerText = Question.Answers.First().Value;
                    break;

                case QuestionType.Number:
                    AnswerNumber = decimal.Parse(Question.Answers.First().Value);
                    break;

                case QuestionType.Date:
                    AnswerDate = DateTime.Parse(Question.Answers.First().Value);
                    break;
            }
        }*/
    }

    public class AnswerSavedEventArgs(string answer, Guid questionId) : EventArgs
    {
        public string Answer { get; } = answer;
        public Guid QuestionId { get; } = questionId;
    }
}