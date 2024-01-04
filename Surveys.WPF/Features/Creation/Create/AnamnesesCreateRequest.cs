using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Exceptions;
using Surveys.WPF.Features.Authentication;

namespace Surveys.WPF.Features.Creation.Create;

public record AnamnesesCreateRequest(List<AnamnesisTemplateDto> Template) : IRequest<OperationResult<List<Domain.Anamnesis>>>;

public class AnamnesesCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<AnamnesesCreateRequest, OperationResult<List<Domain.Anamnesis>>>
{
    public async Task<OperationResult<List<Domain.Anamnesis>>> Handle(AnamnesesCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<Domain.Anamnesis>> operation = OperationResult.CreateResult<List<Domain.Anamnesis>>();
        IRepository<Domain.Anamnesis> repository = unitOfWork.GetRepository<Domain.Anamnesis>();

        List<Domain.Anamnesis> anamnesis = request.Template
            .Where(x => x.IsSelected)
            .Select(x => new Domain.Anamnesis
            {
                AnamnesisTemplateId = x.Id,
                Answers = x.Questions.Select(question => new AnamnesisAnswer
                    {
                        Question = question,
                        Answers = new List<Answer>()
                    })
                    .ToList(),
                CreatedBy = authenticationStore.User?.DisplayName ?? "Unknown"
            })
            .ToList();

        await repository.InsertAsync(anamnesis, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            operation.AddError(unitOfWork.LastSaveChangesResult.Exception
                               ?? new SurveysDatabaseSaveException(nameof(Domain.Anamnesis)));

            return operation;
        }

        operation.Result = anamnesis;

        return operation;
    }
}