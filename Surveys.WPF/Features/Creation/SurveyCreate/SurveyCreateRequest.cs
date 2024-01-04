using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Features.Authentication;

namespace Surveys.WPF.Features.Creation.SurveyCreate;

public record SurveyCreateRequest(List<SurveyDto> Template) : IRequest<OperationResult<List<Anamnesis>>>;

public class SurveyCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<SurveyCreateRequest, OperationResult<List<Anamnesis>>>
{
    public async Task<OperationResult<List<Anamnesis>>> Handle(SurveyCreateRequest request, CancellationToken cancellationToken) => null;
}