using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Domain;
using Surveys.Blazor.Endpoints.QuestionEndpoints.Components;
using Surveys.Blazor.Services;
using Surveys.Web.Application.Messaging.AnamnesisMessages;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;
using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.Components;

public partial class AnamnesisCard
{
    [Parameter] public required AnamnesisViewModel Anamnesis { get; set; }

    [Parameter] public EventCallback<ResponseViewModel> OnChanged { get; set; }

    [Inject] public IAuthorizedHttpClient Client { get; set; } = null!;

    private AnamnesisTemplateViewModel? AnamnesisTemplate => Anamnesis.AnamnesisTemplate;

    private async Task OnAnswerSaved(QuestionCard.AnswerSavedEventArgs args)
    {
        ResponseViewModel? model = Anamnesis.Responses!.FirstOrDefault(response => response.QuestionId == args.QuestionId);

        if (model == null)
        {
            HttpResponseMessage response = await Client.PostAsync("response", new ResponseCreateViewModel
            {
                AnamnesisId = Anamnesis.Id,
                QuestionId = args.QuestionId
            });

            Operation<ResponseViewModel>? survey = await response.Content.ReadFromJsonAsync<Operation<ResponseViewModel>>();

            if (survey is not { Ok: true })
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return;
            }

            model = survey.Result;
        }

        ResponseAnswerViewModel responseAnswer = model.Answers.FirstOrDefault()
                                                 ?? new ResponseAnswerViewModel
                                                 {
                                                     Value = args.Answer
                                                 };

        if (responseAnswer.Value != args.Answer)
            responseAnswer.Value = args.Answer;

        model.Answers.Add(responseAnswer);

        Console.WriteLine($"Ответ сохранен: {responseAnswer.Value}");
        await OnChanged.InvokeAsync(model);
    }
}