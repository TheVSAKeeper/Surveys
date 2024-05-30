using Calabonga.PagedListCore;
using Surveys.Web.Application.Messaging.QuestionMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.QuestionMessages;

/// <summary>
///     Mapper Configuration for entity Question
/// </summary>
public class QuestionMapperConfiguration : Profile
{
    /// <inheritdoc />
    public QuestionMapperConfiguration()
    {
        CreateMap<QuestionCreateViewModel, Question>()
            .ForMember(question => question.Id, expression => expression.Ignore())
            .ForMember(question => question.AnamnesisTemplate, expression => expression.Ignore())
            .ForMember(question => question.AnamnesisAnswers, expression => expression.Ignore());

        CreateMap<Question, QuestionViewModel>();

        CreateMap<Question, QuestionUpdateViewModel>();

        CreateMap<QuestionUpdateViewModel, Question>()
            .ForMember(question => question.AnamnesisAnswers, expression => expression.Ignore());

        CreateMap<IPagedList<Question>, IPagedList<QuestionViewModel>>()
            .ConvertUsing<PagedListConverter<Question, QuestionViewModel>>();
    }
}