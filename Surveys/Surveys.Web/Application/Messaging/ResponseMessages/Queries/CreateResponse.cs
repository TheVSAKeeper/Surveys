using System.Security.Claims;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.Queries;

public sealed class CreateResponse
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<ResponseViewModel, string>>
    {
        public async Task<Operation<ResponseViewModel, string>> Handle(Request responseRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new Response");

            Response? entity = mapper.Map<ResponseCreateViewModel, Response>(responseRequest.Model,
                options => options.Items[nameof(ApplicationUser)] = responseRequest.User.Identity!.Name);

            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppData.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<Response>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                ResponseViewModel? mapped = mapper.Map<Response, ResponseViewModel>(entity);

                if (mapped is null)
                    return Operation.Error(AppData.Exceptions.MappingException);

                logger.LogInformation("New entity {@Response} successfully created", entity);
                return Operation.Result(mapped);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }

    public record Request(ResponseCreateViewModel Model, ClaimsPrincipal User) : IRequest<Operation<ResponseViewModel, string>>;
}