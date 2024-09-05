using Microsoft.AspNetCore.Mvc;
using Surveys.Web.Application.Messaging.AnamnesisMessages;
using Surveys.Web.Application.Messaging.AnamnesisMessages.Queries;

namespace Surveys.Web.Endpoints;

public sealed class AnamnesisEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapAnamnesisEndpoints();
    }
}

internal static class AnamnesisEndpointsExtensions
{
    public static void MapAnamnesisEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/anamneses/").WithTags(nameof(Anamnesis));

        group.MapGet("paged/{pageIndex:int}", async ([FromServices] IMediator mediator, int pageIndex, string? search, HttpContext context, int pageSize = 10)
                => await mediator.Send(new GetAnamnesisPaged.Request(pageIndex, pageSize, search), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapGet("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetAnamnesisById.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapDelete("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new DeleteAnamnesis.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPost("", async ([FromServices] IMediator mediator, AnamnesisCreateViewModel model, HttpContext context)
                => await mediator.Send(new CreateAnamnesis.Request(model, context.User), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPut("{id:guid}", async ([FromServices] IMediator mediator, Guid id, AnamnesisUpdateViewModel model, HttpContext context)
                => await mediator.Send(new UpdateAnamnesis.Request(id, model, context.User), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();
    }
}
