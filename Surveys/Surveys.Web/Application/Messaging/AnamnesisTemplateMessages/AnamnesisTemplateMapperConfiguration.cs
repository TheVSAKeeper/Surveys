using AutoMapper;
using Calabonga.PagedListCore;
using Surveys.Domain;
using Surveys.Web.Application.Messaging.AnamnesisTemplateMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.AnamnesisTemplateMessages;

public class AnamnesisTemplateMapperConfiguration : Profile
{
    public AnamnesisTemplateMapperConfiguration()
    {
        CreateMap<AnamnesisTemplateCreateViewModel, AnamnesisTemplate>()
            .ForMember(template => template.Name, expression => expression.Ignore())
            .ForMember(template => template.SortIndex, expression => expression.Ignore())
            .ForMember(template => template.Questions, expression => expression.Ignore())
            .ForMember(template => template.Anamneses, expression => expression.Ignore())
            ;

        CreateMap<AnamnesisTemplate, AnamnesisTemplateViewModel>()
            .ForMember(template => template.IsSelected, expression => expression
                .MapFrom(template => false))
            ;

        CreateMap<AnamnesisTemplate, AnamnesisTemplateUpdateViewModel>();

        CreateMap<AnamnesisTemplateUpdateViewModel, AnamnesisTemplate>()
            .ForMember(template => template.Name, expression => expression.Ignore())
            .ForMember(template => template.SortIndex, expression => expression.Ignore())
            .ForMember(template => template.Questions, expression => expression.Ignore())
            .ForMember(template => template.Anamneses, expression => expression.Ignore())
            ;

        CreateMap<IPagedList<AnamnesisTemplate>, IPagedList<AnamnesisTemplateViewModel>>()
            .ConvertUsing<PagedListConverter<AnamnesisTemplate, AnamnesisTemplateViewModel>>();
    }
}