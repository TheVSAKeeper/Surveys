using AutoMapper;
using Calabonga.PagedListCore;
using Surveys.Domain;
using Surveys.Web.Application.Messaging.EventItemMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.EventItemMessages;

/// <summary>
///     Mapper Configuration for entity EventItem
/// </summary>
public class EventItemMapperConfiguration : Profile
{
    /// <inheritdoc />
    public EventItemMapperConfiguration()
    {
        CreateMap<EventItemCreateViewModel, EventItem>()
            .ForMember(eventItem => eventItem.Id, expression => expression.Ignore());

        CreateMap<EventItem, EventItemViewModel>();

        CreateMap<EventItem, EventItemUpdateViewModel>();

        CreateMap<EventItemUpdateViewModel, EventItem>()
            .ForMember(eventItem => eventItem.CreatedAt, expression => expression.Ignore())
            .ForMember(eventItem => eventItem.ThreadId, expression => expression.Ignore())
            .ForMember(eventItem => eventItem.ExceptionMessage, expression => expression.Ignore());

        CreateMap<IPagedList<EventItem>, IPagedList<EventItemViewModel>>()
            .ConvertUsing<PagedListConverter<EventItem, EventItemViewModel>>();
    }
}