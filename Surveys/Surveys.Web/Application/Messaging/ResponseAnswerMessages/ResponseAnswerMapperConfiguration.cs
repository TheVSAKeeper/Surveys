using Surveys.Web.Application.Messaging.ResponseAnswerMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ResponseAnswerMessages;

public class ResponseAnswerMapperConfiguration : Profile
{
    public ResponseAnswerMapperConfiguration()
    {
        CreateMap<ResponseAnswerCreateViewModel, ResponseAnswer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Response, opt => opt.Ignore())
            .ForMember(dest => dest.Question, opt => opt.Ignore())
            ;

        CreateMap<ResponseAnswer, ResponseAnswerViewModel>();

        CreateMap<ResponseAnswer, ResponseAnswerUpdateViewModel>();

        CreateMap<ResponseAnswerUpdateViewModel, ResponseAnswer>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.ResponseId, opt => opt.Ignore())
            .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
            .ForMember(dest => dest.Response, opt => opt.Ignore())
            .ForMember(dest => dest.Question, opt => opt.Ignore())
            ;
    }
}