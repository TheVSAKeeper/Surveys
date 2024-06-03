using Calabonga.PagedListCore;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.ResponseMessages;

public class ResponseMapperConfiguration : Profile
{
    public ResponseMapperConfiguration()
    {
        CreateMap<ResponseCreateViewModel, Response>()
            .ForMember(survey => survey.Id, expression => expression.Ignore())
            .ForMember(survey => survey.Anamnesis, expression => expression.Ignore())
            .ForMember(survey => survey.CreatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.CreatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(survey => survey.UpdatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedBy, expression => expression.Ignore())
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

        CreateMap<Response, ResponseViewModel>()
            .ForMember(dest => dest.Anamnesis, expression => expression.MapFrom(src => src.Anamnesis))
            .ForMember(dest => dest.Answers, expression => expression.MapFrom(src => src.Answers));

        CreateMap<Response, ResponseUpdateViewModel>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

        CreateMap<ResponseUpdateViewModel, Response>()
            .ForMember(survey => survey.CreatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.CreatedBy, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedAt, expression => expression.Ignore())
            .ForMember(survey => survey.UpdatedBy, expression => expression
                .MapFrom((_, _, _, context) => context.Items[nameof(ApplicationUser)]))
            .ForMember(survey => survey.AnamnesisId, expression => expression.Ignore())
            .ForMember(survey => survey.Anamnesis, expression => expression.Ignore())
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

        CreateMap<IPagedList<Response>, IPagedList<ResponseViewModel>>()
            .ConvertUsing<PagedListConverter<Response, ResponseViewModel>>();
    }
}