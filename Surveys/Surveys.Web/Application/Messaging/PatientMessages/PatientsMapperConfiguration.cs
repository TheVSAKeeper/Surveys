using AutoMapper;
using Calabonga.PagedListCore;
using Surveys.Domain;
using Surveys.Web.Application.Messaging.PatientMessages.ViewModels;
using Surveys.Web.Definitions.Mapping;

namespace Surveys.Web.Application.Messaging.PatientMessages;

public class PatientMapperConfiguration : Profile
{
    public PatientMapperConfiguration()
    {
        CreateMap<PatientCreateViewModel, Patient>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Surveys, o => o.Ignore())
            .ForMember(x => x.SurveyDiagnoses, o => o.Ignore());

        CreateMap<Patient, PatientViewModel>();

        CreateMap<IPagedList<Patient>, IPagedList<PatientViewModel>>()
            .ConvertUsing<PagedListConverter<Patient, PatientViewModel>>();
    }
}