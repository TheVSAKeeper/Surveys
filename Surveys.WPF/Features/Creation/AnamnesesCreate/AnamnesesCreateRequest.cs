using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Exceptions;
using Surveys.WPF.Features.Authentication;

namespace Surveys.WPF.Features.Creation.AnamnesesCreate;

public record AnamnesesCreateRequest(List<AnamnesisTemplateDto> Template) : IRequest<OperationResult<List<Anamnesis>>>;

public class AnamnesesCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<AnamnesesCreateRequest, OperationResult<List<Anamnesis>>>
{
    public async Task<OperationResult<List<Anamnesis>>> Handle(AnamnesesCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<Anamnesis>> operation = OperationResult.CreateResult<List<Anamnesis>>();
        IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();

        List<Anamnesis> anamnesis = request.Template
            .Where(x => x.IsSelected)
            .Select(x => new Anamnesis
            {
                AnamnesisTemplateId = x.Id,
                AnamnesisAnswers = x.Questions.Select(question => new AnamnesisAnswer
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
                               ?? new SurveysDatabaseSaveException(nameof(Anamnesis)));

            return operation;
        }

        operation.Result = anamnesis;

        return operation;
    }
}