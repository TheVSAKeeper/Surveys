using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.WPF.Features.Authentication.Update;

namespace Surveys.WPF.Features.Creation.Create;

public record AnamnesesCreateRequest() : IRequest<OperationResult<Domain.Anamnesis>>;

public class AnamnesesCreateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AnamnesesCreateRequest, OperationResult<Domain.Anamnesis>>
{
    public async Task<OperationResult<Domain.Anamnesis>> Handle(AnamnesesCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<Domain.Anamnesis> operation = OperationResult.CreateResult<Domain.Anamnesis>();

        return operation;
    }
}