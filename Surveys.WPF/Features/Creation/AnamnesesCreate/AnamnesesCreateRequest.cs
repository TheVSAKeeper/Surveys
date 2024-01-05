﻿using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Exceptions;
using Surveys.WPF.Features.Authentication;
using SurveysArgumentNullException = Surveys.Domain.Exceptions.SurveysArgumentNullException;

namespace Surveys.WPF.Features.Creation.AnamnesesCreate;

public record AnamnesesCreateRequest(Survey? SurveyToAdd, List<AnamnesisTemplateDto> Template) : IRequest<OperationResult<List<Anamnesis>>>;

public class AnamnesesCreateRequestHandler(IUnitOfWork unitOfWork, AuthenticationStore authenticationStore) : IRequestHandler<AnamnesesCreateRequest, OperationResult<List<Anamnesis>>>
{
    public async Task<OperationResult<List<Anamnesis>>> Handle(AnamnesesCreateRequest request, CancellationToken cancellationToken)
    {
        OperationResult<List<Anamnesis>> result = OperationResult.CreateResult<List<Anamnesis>>();

        if (request.SurveyToAdd is null)
        {
            result.AddError(new SurveysArgumentNullException(nameof(request.SurveyToAdd)));
            return result;
        }

        IRepository<Anamnesis> repository = unitOfWork.GetRepository<Anamnesis>();

        Survey? survey = await unitOfWork.GetRepository<Survey>()
            .GetFirstOrDefaultAsync(predicate: p => p.Id == request.SurveyToAdd.Id);
        
        if (survey is null)
        {
            result.AddError(new SurveysArgumentNullException(nameof(survey)));
            return result;
        }

        List<Anamnesis> anamnesis = request.Template
            .Where(x => x.IsSelected)
            .Select(x => new Anamnesis
            {
                Survey = survey,
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
            result.AddError(unitOfWork.LastSaveChangesResult.Exception
                            ?? new SurveysDatabaseSaveException(nameof(Anamnesis)));

            return result;
        }

        result.Result = anamnesis;

        return result;
    }
}