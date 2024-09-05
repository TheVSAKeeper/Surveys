using Calabonga.PagedListCore;
using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages;

public class ResponseAnswerMapperConfiguration : Profile
{
    public ResponseAnswerMapperConfiguration()
    {
        CreateMap<ResponseAnswerCreateViewModel, ResponseAnswer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Response, opt => opt.Ignore())
            ;

        CreateMap<ResponseAnswer, ResponseAnswerViewModel>()
            ;

        CreateMap<ResponseAnswer, ResponseAnswerUpdateViewModel>();

        CreateMap<ResponseAnswerUpdateViewModel, ResponseAnswer>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.ResponseId, opt => opt.Ignore())
            .ForMember(dest => dest.Response, opt => opt.Ignore())
            ;

        CreateMap<IPagedList<ResponseAnswer>, IPagedList<ResponseAnswerViewModel>>()
            .ConvertUsing<PagedListConverter<ResponseAnswer, ResponseAnswerViewModel>>();
    }
}
