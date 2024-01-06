using System.Collections.ObjectModel;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.WPF.Features.Creation.AnamnesesCreate;

namespace Surveys.WPF.Features.Search.PatientSearch;

public record GetAllPatientsRequest : IRequest<OperationResult<List<Patient>>>;

public class GetAllPatientsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllPatientsRequest, OperationResult<List<Patient>>>
{
    public async Task<OperationResult<List<Patient>>> Handle(GetAllPatientsRequest request, CancellationToken cancellationToken)
    {
        IList<Patient> templates = await unitOfWork.GetRepository<Patient>().GetAllAsync(disableTracking: true);
        
        return OperationResult.CreateResult(templates.ToList());
    }
}