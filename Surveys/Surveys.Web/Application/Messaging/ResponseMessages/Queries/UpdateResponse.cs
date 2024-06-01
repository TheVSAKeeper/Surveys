using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseMessages.Queries;

public sealed class UpdateResponse
{
    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<ResponseViewModel, string>>
    {
        public async Task<Operation<ResponseViewModel, string>> Handle(Request responseRequest, CancellationToken cancellationToken)
        {
            IRepository<Response> repository = unitOfWork.GetRepository<Response>();

            Response? entity = await repository.GetFirstOrDefaultAsync(predicate: response => response.Id == responseRequest.Id, disableTracking: false);

            if (entity == null)
                return Operation.Error(AppData.Exceptions.NotFoundException);

            mapper.Map(responseRequest.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            SaveChangesResult lastResult = unitOfWork.LastSaveChangesResult;

            if (lastResult.IsOk)
            {
                ResponseViewModel? mapped = mapper.Map<Response, ResponseViewModel>(entity);

                if (mapped is not null)
                    return Operation.Result(mapped);

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            string errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }

    public record Request(Guid Id, ResponseUpdateViewModel Model) : IRequest<Operation<ResponseViewModel, string>>;
}