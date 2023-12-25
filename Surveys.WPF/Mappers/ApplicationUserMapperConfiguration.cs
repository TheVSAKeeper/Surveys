using AutoMapper;
using Surveys.Infrastructure;
using Surveys.WPF.Features.Authentication.EditProfile;

namespace Surveys.WPF.Mappers;

public class ApplicationUserMapperConfiguration : Profile
{
    public ApplicationUserMapperConfiguration()
    {
        CreateMap<ApplicationUser, UserUpdateViewModel>();
    }
}