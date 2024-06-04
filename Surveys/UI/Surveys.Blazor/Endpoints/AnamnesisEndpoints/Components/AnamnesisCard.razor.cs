using Microsoft.AspNetCore.Components;
using Surveys.Blazor.Endpoints.QuestionEndpoints.Components;
using Surveys.Web.Application.Messaging.AnamnesisMessages;

namespace Surveys.Blazor.Endpoints.AnamnesisEndpoints.Components;

public partial class AnamnesisCard
{
    [Parameter] public required AnamnesisViewModel Anamnesis { get; set; }

    private void OnAnswerSaved(QuestionCard.AnswerSavedEventArgs e)
    {
        Console.WriteLine($"Ответ сохранен: {e.Answer}");
    }
}