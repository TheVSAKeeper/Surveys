using Microsoft.AspNetCore.Mvc;
using Surveys.Web.Application.Messaging.SurveyMessages.Queries;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;

namespace Surveys.Web.Endpoints;

public sealed class SurveyEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app) => app.MapSurveyEndpoints();
}

internal static class SurveyEndpointsExtensions
{
    public static void MapSurveyEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/surveys/").WithTags(nameof(Survey));

        group.MapGet("paged/{pageIndex:int}", async ([FromServices] IMediator mediator, int pageIndex, string? search, Guid? patientId, HttpContext context, int pageSize = 10)
                => await mediator.Send(new GetSurveyPaged.Request(pageIndex, pageSize, search, patientId), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapGet("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetSurveyById.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapGet("get-for-edit/{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetSurveyForUpdateById.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapDelete("{id:guid}", async ([FromServices] IMediator mediator, Guid id,HttpContext context)
                => await mediator.Send(new DeleteSurvey.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPost("", async ([FromServices] IMediator mediator, SurveyCreateViewModel model, bool? isDefault,  HttpContext context)
                => await mediator.Send(new CreateSurvey.Request(model, context.User, isDefault), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPut("{id:guid}", async ([FromServices] IMediator mediator, Guid id, SurveyUpdateViewModel model, HttpContext context)
                => await mediator.Send(new UpdateSurvey.Request(id, model, context.User), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();
    }
}