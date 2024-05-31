using Calabonga.PagedListCore;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.SurveyMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.SurveyMessages;

public class SurveyMapperConfiguration : Profile
{
    public SurveyMapperConfiguration()
    {
        CreateMap<SurveyCreateViewModel, Survey>()
            .ForMember(survey => survey.CreatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.CreatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(survey => survey.UpdatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedBy, expression => expression.Ignore())
            .ForMember(survey => survey.Patient, expression => expression.Ignore())
            .ForMember(survey => survey.IsComplete, expression => expression.Ignore())
            .ForMember(survey => survey.SurveyDiagnoses, expression => expression.Ignore())
            .ForMember(survey => survey.Anamneses, expression => expression.Ignore())
            ;

        CreateMap<Survey, SurveyViewModel>()
            // .ForMember(survey => survey.IsSelected, expression => expression
            //     .MapFrom(survey => false))
            ;

        CreateMap<Survey, SurveyUpdateViewModel>();

        CreateMap<SurveyUpdateViewModel, Survey>()
            .ForMember(survey => survey.CreatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.CreatedBy, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(survey => survey.PatientId, expression => expression.Ignore())
            .ForMember(survey => survey.Patient, expression => expression.Ignore())
            .ForMember(survey => survey.IsComplete, expression => expression.Ignore())
            .ForMember(survey => survey.SurveyDiagnoses, expression => expression.Ignore())
            .ForMember(survey => survey.Anamneses, expression => expression.Ignore())
            // .ForMember(survey => survey.Name, expression => expression.Ignore())
            // .ForMember(survey => survey.SortIndex, expression => expression.Ignore())
            // .ForMember(survey => survey.Questions, expression => expression.Ignore())
            // .ForMember(survey => survey.Anamneses, expression => expression.Ignore())
            ;

        CreateMap<IPagedList<Survey>, IPagedList<SurveyViewModel>>()
            .ConvertUsing<PagedListConverter<Survey, SurveyViewModel>>();
    }
}