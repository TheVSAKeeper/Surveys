using Calabonga.PagedListCore;
using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.QuestionMessages;

public class QuestionMapperConfiguration : Profile
{
    public QuestionMapperConfiguration()
    {
        CreateMap<QuestionCreateViewModel, Question>()
            .ForMember(survey => survey.Id, expression => expression.Ignore())
            .ForMember(survey => survey.Answers, expression => expression.Ignore())
            .ForMember(survey => survey.AnamnesisTemplate, expression => expression.Ignore())
            ;

        CreateMap<Question, QuestionViewModel>()
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

        CreateMap<Question, QuestionUpdateViewModel>();

        CreateMap<QuestionUpdateViewModel, Question>()
            .ForMember(survey => survey.AnamnesisTemplate, expression => expression.Ignore())
            .ForMember(survey => survey.AnamnesisTemplateId, expression => expression.Ignore())
            .ForMember(survey => survey.Answers, expression => expression.Ignore())
            ;

        CreateMap<IPagedList<Question>, IPagedList<QuestionViewModel>>()
            .ConvertUsing<PagedListConverter<Question, QuestionViewModel>>();
    }
}
