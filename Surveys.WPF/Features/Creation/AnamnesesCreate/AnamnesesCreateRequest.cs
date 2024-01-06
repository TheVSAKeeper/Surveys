using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.Domain.Exceptions;
using Surveys.WPF.Features.Authentication;

namespace Surveys.WPF.Features.Creation.AnamnesesCreate;

public record AnamnesesCreateRequest(List<AnamnesisTemplateDto> Template) : IRequest<OperationResult<List<Anamnesis>>>;

public class AnamnesesCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<AnamnesesCreateRequest, OperationResult<List<Anamnesis>>>
{
    public async Task<OperationResult<List<Anamnesis>>> Handle(AnamnesesCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<Anamnesis>> result = OperationResult.CreateResult<List<Anamnesis>>();
        IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();

        List<Anamnesis> anamnesis = request.Template
            .Where(x => x.IsSelected)
            .Select(x => new Anamnesis
            {
                AnamnesisTemplateId = x.Id,
                AnamnesisAnswers = x.Questions.Select(question => new AnamnesisAnswer
                    {
                        QuestionId = question.Id,
                        Answers = new List<Answer>()
                    })
                    .ToList(),
                CreatedBy = authenticationStore.Username
            })
            .ToList();

        await repository.InsertAsync(anamnesis, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            result.AddError(unitOfWork.LastSaveChangesResult.Exception
                            ?? new SurveysDatabaseSaveException(nameof(Anamnesis)));

            return result;
        }

        result.Result = anamnesis;

        return result;
    }
}