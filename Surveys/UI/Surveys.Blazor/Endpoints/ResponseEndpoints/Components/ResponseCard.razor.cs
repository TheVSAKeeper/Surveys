using System.Globalization;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Services;
using Surveys.Domain;
using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;
using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.ResponseEndpoints.Components;

public partial class ResponseCard
{
    [Parameter] public ResponseViewModel Response { get; set; } = null!;
    [Parameter] public EventCallback AnswerChanged { get; set; }
    [Inject] private IAuthorizedHttpClient Client { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;

    private QuestionViewModel Question => Response.Question!;

    private string SelectedOption { get; set; } = string.Empty;
    private string AnswerText { get; set; } = string.Empty;
    private decimal AnswerNumber { get; set; }
    private DateTime AnswerDate { get; set; }

    private ResponseAnswerViewModel? Answer { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Operation<ResponseViewModel>? result = await Client.GetFromJsonAsync<ResponseViewModel>($"response/{Response.Id}");
        Response = result.Result;

        await UpdateCard();
        await base.OnParametersSetAsync();
    }

    private async Task UpdateCard()
    {
        Answer = await GetAnswer();

        switch (Question.Type)
        {
            case QuestionType.SingleChoice:
            case QuestionType.MultipleChoice:
                SelectedOption = Answer.Value;
                break;

            case QuestionType.Text:
                AnswerText = Answer.Value;
                break;

            case QuestionType.Number:
                AnswerNumber = decimal.Parse(Answer.Value);
                break;

            case QuestionType.Date:
                AnswerDate = DateTime.Parse(Answer.Value);
                break;

            case QuestionType.None:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async Task<ResponseAnswerViewModel> GetAnswer()
    {
        List<ResponseAnswerViewModel> answers = Response.Answers;

        if (answers is null)
        {
            Operation<PagedListResult<ResponseAnswerViewModel>>? answersResult = await Client.GetPagedAsync<ResponseAnswerViewModel>(AppData.ResponseAnswerUrl, search: $"{Response.Id}");

            if (answersResult is not { Ok: true })
            {
                throw new HttpRequestException();
            }

            answers = answersResult.Result.Items.ToList();
        }

        if (answers.Count != 0)
        {
            return answers.First();
        }

        Operation<ResponseAnswerViewModel>? answer = await Client.PostFromJsonAsync<ResponseAnswerCreateViewModel,
            ResponseAnswerViewModel>(AppData.ResponseAnswerUrl, new ResponseAnswerCreateViewModel
        {
            Value = string.Empty,
            ResponseId = Response.Id,
            QuestionId = Response.QuestionId
        });

        if (answer is { Ok: true })
        {
            return answer.Result;
        }

        return new ResponseAnswerViewModel
        {
            Value = string.Empty
        };
    }

    private async Task OnAnswerChanged(FocusEventArgs focusEventArgs)
    {
        ResponseAnswerUpdateViewModel answer = GetUpdateAnswer();

        Operation<ResponseAnswerViewModel>? result = await Client.PutFromJsonAsync<ResponseAnswerUpdateViewModel, ResponseAnswerViewModel>($"{AppData.ResponseAnswerUrl}/{answer.Id}",
            answer);

        if (result is { Ok: true })
        {
            Answer = result.Result;
            await ToastService.Success($"Ответ {result.Result.Value} сохранен", "Сохранение");
            await AnswerChanged.InvokeAsync();
        }
    }

    private ResponseAnswerUpdateViewModel GetUpdateAnswer()
    {
        return new ResponseAnswerUpdateViewModel
        {
            Value = Question.Type switch
            {
                QuestionType.SingleChoice or QuestionType.MultipleChoice => SelectedOption,
                QuestionType.Text => AnswerText,
                QuestionType.Number => AnswerNumber.ToString(CultureInfo.InvariantCulture),
                QuestionType.Date => AnswerDate.ToString(CultureInfo.InvariantCulture),
                var _ => string.Empty
            },
            Id = Answer.Id
        };
    }
}
