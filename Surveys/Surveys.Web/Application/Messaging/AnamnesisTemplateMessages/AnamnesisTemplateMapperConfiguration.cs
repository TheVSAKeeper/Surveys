using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages;

public class AnamnesisTemplateMapperConfiguration : Profile
{
    public AnamnesisTemplateMapperConfiguration()
    {
        CreateMap<AnamnesisTemplate, AnamnesisTemplateViewModel>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
            ;

        CreateMap<AnamnesisTemplateCreateViewModel, AnamnesisTemplate>()
            .ForMember(survey => survey.Id, expression => expression.Ignore())
            .ForMember(survey => survey.Anamneses, expression => expression.Ignore())
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

        CreateMap<AnamnesisTemplate, AnamnesisTemplateUpdateViewModel>();

        CreateMap<AnamnesisTemplateUpdateViewModel, AnamnesisTemplate>()
            .ForMember(survey => survey.Questions, expression => expression.Ignore())
            .ForMember(survey => survey.Anamneses, expression => expression.Ignore())
            ;
    }
}