using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public record GetAllSurveysRequest : IRequest<OperationResult<List<SurveyDto>>>;

public class GetAllSurveysRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllSurveysRequest, OperationResult<List<SurveyDto>>>
{
    public async Task<OperationResult<List<SurveyDto>>> Handle(GetAllSurveysRequest request, CancellationToken cancellationToken) => null;
}