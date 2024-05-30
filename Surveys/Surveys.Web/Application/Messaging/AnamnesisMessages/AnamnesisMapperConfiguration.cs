using Calabonga.PagedListCore;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.AnamnesisMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages;

public class AnamnesisMapperConfiguration : Profile
{
    public AnamnesisMapperConfiguration()
    {
        CreateMap<AnamnesisCreateViewModel, Anamnesis>()
            .ForMember(anamnesis => anamnesis.CreatedAt, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.CreatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(anamnesis => anamnesis.UpdatedAt, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.UpdatedBy, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.AnamnesisTemplate, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.AnamnesisAnswers, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.IsComplete, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.Survey, expression => expression.Ignore())
            ;

        CreateMap<Anamnesis, AnamnesisViewModel>()
            // .ForMember(anamnesis => anamnesis.IsSelected, expression => expression
            //     .MapFrom(anamnesis => false))
            ;

        CreateMap<Anamnesis, AnamnesisUpdateViewModel>();

        CreateMap<AnamnesisUpdateViewModel, Anamnesis>()
            .ForMember(anamnesis => anamnesis.CreatedAt, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.CreatedBy, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.UpdatedAt, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.UpdatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(anamnesis => anamnesis.AnamnesisTemplateId, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.AnamnesisTemplate, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.AnamnesisAnswers, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.IsComplete, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.SortIndex, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.SurveyId, expression => expression.Ignore())
            .ForMember(anamnesis => anamnesis.Survey, expression => expression.Ignore())
            ;

        CreateMap<IPagedList<Anamnesis>, IPagedList<AnamnesisViewModel>>()
            .ConvertUsing<PagedListConverter<Anamnesis, AnamnesisViewModel>>();
    }
}