using Microsoft.AspNetCore.Mvc;
using Surveys.Web.Application.Messaging.PatientMessages.Queries;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;

namespace Surveys.Web.Endpoints;

public sealed class PatientEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapPatientEndpoints();
    }
}

internal static class PatientEndpointsExtensions
{
    public static void MapPatientEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/patients/").WithTags(nameof(Patient));

        group.MapGet("paged/{pageIndex:int}", async ([FromServices] IMediator mediator, int pageIndex, string? search, HttpContext context, int pageSize = 10)
                => await mediator.Send(new GetPatientPaged.Request(pageIndex, pageSize, search), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapGet("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new GetPatientById.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapDelete("{id:guid}", async ([FromServices] IMediator mediator, Guid id, HttpContext context)
                => await mediator.Send(new DeletePatient.Request(id), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();

        group.MapPost("", async ([FromServices] IMediator mediator, PatientCreateViewModel model, HttpContext context)
                => await mediator.Send(new CreatePatient.Request(model), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .Produces(200)
            .ProducesProblem(401)
            .ProducesProblem(404)
            .WithOpenApi();
    }
}
