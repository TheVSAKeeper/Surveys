using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;

namespace Surveys.WPF.Endpoints.PatientsEndpoints.Search;

public record GetAllPatientsRequest : IRequest<OperationResult<List<Patient>>>;

public class GetAllPatientsRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPatientsRequest, OperationResult<List<Patient>>>
{
    public async Task<OperationResult<List<Patient>>> Handle(GetAllPatientsRequest request, CancellationToken cancellationToken)
    {
        IList<Patient> patients = await unitOfWork.GetRepository<Patient>().GetAllAsync(disableTracking: true);

        return OperationResult.CreateResult(patients.ToList());
    }
}