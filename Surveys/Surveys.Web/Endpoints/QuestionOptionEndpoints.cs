using Microsoft.AspNetCore.Mvc;
using Surveys.Web.Application.Messaging.QuestionOptionMessages.Queries;
using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;

namespace Surveys.Web.Endpoints;

public sealed class QuestionOptionEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapQuestionOptionEndpoints();
    }
}

internal static class QuestionOptionEndpointsExtensions
{
    public static void MapQuestionOptionEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/question-option/").WithTags(nameof(QuestionOption));

        group.MapGet("paged/{pageIndex:int}", async ([FromServices] IMediator mediator, int pageIndex, string? search, HttpContext context, int pageSize = 10)
                => await mediator.Send(new GetQuestionOptionPaged.Request(pageIndex, pageSize, search), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapGet("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetQuestionOptionById.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapDelete("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new DeleteQuestionOption.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPost("", async ([FromServices] IMediator mediator, QuestionOptionCreateViewModel model, HttpContext context)
                => await mediator.Send(new CreateQuestionOption.Request(model), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPut("{id:guid}", async ([FromServices] IMediator mediator, Guid id, QuestionOptionUpdateViewModel model, HttpContext context)
                => await mediator.Send(new UpdateQuestionOption.Request(id, model), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();
    }
}
