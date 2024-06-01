using Surveys.Infrastructure;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages;

public class AnamnesisMapperConfiguration : Profile
{
    public AnamnesisMapperConfiguration()
    {
        CreateMap<AnamnesisCreateViewModel, Anamnesis>()
            .ForMember(anamnesis => anamnesis.CreatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.CreatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(survey => survey.UpdatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedBy, expression => expression.Ignore())
            .ForMember(dest => dest.Survey, expression => expression.Ignore())
            .ForMember(dest => dest.AnamnesisTemplate, expression => expression.Ignore())
            .ForMember(dest => dest.Responses, expression => expression.Ignore())
            .ForMember(dest => dest.IsComplete, expression => expression.Ignore())
            ;

        CreateMap<Anamnesis, AnamnesisViewModel>()
            .ForMember(dest => dest.AnamnesisTemplate, expression => expression.MapFrom(src => src.AnamnesisTemplate))
            .ForMember(dest => dest.Responses, expression => expression.MapFrom(src => src.Responses));

        CreateMap<Anamnesis, AnamnesisUpdateViewModel>();

        CreateMap<AnamnesisUpdateViewModel, Anamnesis>()
            .ForMember(survey => survey.CreatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.CreatedBy, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(dest => dest.SurveyId, expression => expression.Ignore())
            .ForMember(dest => dest.Survey, expression => expression.Ignore())
            .ForMember(dest => dest.AnamnesisTemplateId, expression => expression.Ignore())
            .ForMember(dest => dest.AnamnesisTemplate, expression => expression.Ignore())
            .ForMember(dest => dest.Responses, expression => expression.MapFrom(src => src.Responses))
            ;
    }
}