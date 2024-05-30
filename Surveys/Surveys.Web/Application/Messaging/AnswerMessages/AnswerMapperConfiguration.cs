using Calabonga.PagedListCore;
using Surveys.Web.Application.Messaging.AnswerMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.AnswerMessages;

public class AnswerMapperConfiguration : Profile
{
    public AnswerMapperConfiguration()
    {
        CreateMap<AnswerCreateViewModel, Answer>()
            .ForMember(template => template.Content, expression => expression.Ignore())
            .ForMember(template => template.AnamnesisAnswers, expression => expression.Ignore())
            .ForMember(template => template.AnamnesisAnswersId, expression => expression.Ignore())
            ;

        CreateMap<Answer, AnswerViewModel>()
            ;

        CreateMap<Answer, AnswerUpdateViewModel>();

        CreateMap<AnswerUpdateViewModel, Answer>()
            .ForMember(template => template.Content, expression => expression.Ignore())
            .ForMember(template => template.AnamnesisAnswers, expression => expression.Ignore())
            .ForMember(template => template.AnamnesisAnswersId, expression => expression.Ignore())
            ;

        CreateMap<IPagedList<Answer>, IPagedList<AnswerViewModel>>()
            .ConvertUsing<PagedListConverter<Answer, AnswerViewModel>>();
    }
}