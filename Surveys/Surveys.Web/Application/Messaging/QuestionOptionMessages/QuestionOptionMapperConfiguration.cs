using Calabonga.PagedListCore;
using Surveys.Web.Application.Messaging.QuestionOptionMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.QuestionOptionMessages;

public class QuestionOptionMapperConfiguration : Profile
{
    public QuestionOptionMapperConfiguration()
    {
        CreateMap<QuestionOptionCreateViewModel, QuestionOption>()
            .ForMember(survey => survey.Id, expression => expression.Ignore())
            .ForMember(survey => survey.Question, expression => expression.Ignore())
            ;

        CreateMap<QuestionOption, QuestionOptionViewModel>();

        CreateMap<QuestionOption, QuestionOptionUpdateViewModel>();

        CreateMap<QuestionOptionUpdateViewModel, QuestionOption>()
            .ForMember(survey => survey.Question, expression => expression.Ignore())
            .ForMember(survey => survey.QuestionId, expression => expression.Ignore())
            ;

        CreateMap<IPagedList<QuestionOption>, IPagedList<QuestionOptionViewModel>>()
            .ConvertUsing<PagedListConverter<QuestionOption, QuestionOptionViewModel>>();
    }
}