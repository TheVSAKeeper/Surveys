using AutoMapper;
using Surveys.Domain;
using Surveys.Infrastructure;
using Surveys.WPF.Endpoints.SurveysEndpoints.Edit;

namespace Surveys.WPF.Mappers;

public class SurveyMapperConfiguration : Profile
{
    public SurveyMapperConfiguration()
    {
        CreateMap<Survey, SurveyEditDto>();

        CreateMap<SurveyEditDto, Survey>()
            .ForMember(survey => survey.SurveyDiagnoses, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]));
    }
}