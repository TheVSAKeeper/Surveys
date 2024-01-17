using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;
using Surveys.Domain.Exceptions;
using Surveys.WPF.Endpoints.AuthenticationEndpoints;

namespace Surveys.WPF.Endpoints.AnamnesesEndpoints.Create;

public record AnamnesesCreateRequest(List<AnamnesisTemplateDto> Template) : IRequest<OperationResult<List<Anamnesis>>>;

public class AnamnesesCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<AnamnesesCreateRequest, OperationResult<List<Anamnesis>>>
{
    public async Task<OperationResult<List<Anamnesis>>> Handle(AnamnesesCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<Anamnesis>> result = OperationResult.CreateResult<List<Anamnesis>>();
        IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();

        List<Anamnesis> anamneses = request.Template
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
                CreatedBy = authenticationStore.Username,
                SortIndex = x.SortIndex
            })
            .ToList();

        await repository.InsertAsync(anamneses, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        if (unitOfWork.LastSaveChangesResult.IsOk == false)
        {
            result.AddError(unitOfWork.LastSaveChangesResult.Exception
                            ?? new SurveysDatabaseSaveException(nameof(Anamnesis)));

            return result;
        }

        List<Guid> keys = anamneses.Select(x => x.Id).ToList();

        IList<Anamnesis> entities = await repository.GetAllAsync(predicate: anamnesis => keys.Contains(anamnesis.Id),
            include: i => i
                .Include(anamnesis => anamnesis.AnamnesisAnswers)
                .AsSplitQuery()
                .Include(anamnesis => anamnesis.AnamnesisTemplate)!);

        result.Result = entities.ToList();

        return result;
    }
}