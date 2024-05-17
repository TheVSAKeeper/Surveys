using Calabonga.AspNetCore.AppDefinitions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Surveys.Domain;
using Surveys.Domain.Base;
using Surveys.Web.Application.Messaging.QuestionMessages.Queries;
using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;

namespace Surveys.Web.Endpoints;

public sealed class QuestionEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app) => app.MapQuestionEndpoints();
}

internal static class QuestionEndpointsExtensions
{
    public static void MapQuestionEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/questions/").WithTags(nameof(Question));

        group.MapGet("paged/{pageIndex:int}", async ([FromServices] IMediator mediator, int pageIndex, string? search, HttpContext context, int pageSize = 10)
                => await mediator.Send(new GetQuestionPaged.Request(pageIndex, pageSize, search), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapGet("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetQuestionById.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapDelete("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new DeleteQuestion.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPost("", async ([FromServices] IMediator mediator, QuestionCreateViewModel model, HttpContext context)
                => await mediator.Send(new CreateQuestion.Request(model), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPut("{id:guid}", async ([FromServices] IMediator mediator, Guid id, QuestionUpdateViewModel model, HttpContext context)
                => await mediator.Send(new UpdateQuestion.Request(id, model), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();
    }
}