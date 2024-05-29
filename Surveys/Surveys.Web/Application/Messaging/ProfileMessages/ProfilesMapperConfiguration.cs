using System.Security.Claims;
using AutoMapper;
using Calabonga.Microservices.Core;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.ProfileMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.ProfileMessages;

public sealed class ProfilesMapperConfiguration : Profile
{
    public ProfilesMapperConfiguration()
    {
        CreateMap<RegisterViewModel, ApplicationUser>()
            .ForMember(user => user.UserName, expression => expression.MapFrom(registerViewModel => registerViewModel.Email))
            .ForMember(user => user.Email, expression => expression.MapFrom(registerViewModel => registerViewModel.Email))
            .ForMember(user => user.EmailConfirmed, expression => expression.MapFrom(registerViewModel => true))
            .ForMember(user => user.FirstName, expression => expression.MapFrom(registerViewModel => registerViewModel.FirstName))
            .ForMember(user => user.LastName, expression => expression.MapFrom(registerViewModel => registerViewModel.LastName))
            .ForMember(user => user.Patronymic, expression => expression.MapFrom(registerViewModel => registerViewModel.Patronymic))
            .ForMember(user => user.PhoneNumberConfirmed, expression => expression.MapFrom(registerViewModel => true))
            .ForMember(user => user.ApplicationUserProfileId, expression => expression.Ignore())
            .ForMember(user => user.ApplicationUserProfile, expression => expression.Ignore())
            .ForMember(user => user.Id, expression => expression.Ignore())
            .ForMember(user => user.NormalizedUserName, expression => expression.Ignore())
            .ForMember(user => user.NormalizedEmail, expression => expression.Ignore())
            .ForMember(user => user.PasswordHash, expression => expression.Ignore())
            .ForMember(user => user.SecurityStamp, expression => expression.Ignore())
            .ForMember(user => user.ConcurrencyStamp, expression => expression.Ignore())
            .ForMember(user => user.PhoneNumber, expression => expression.Ignore())
            .ForMember(user => user.TwoFactorEnabled, expression => expression.Ignore())
            .ForMember(user => user.LockoutEnd, expression => expression.Ignore())
            .ForMember(user => user.LockoutEnabled, expression => expression.Ignore())
            .ForMember(user => user.AccessFailedCount, expression => expression.Ignore());

        CreateMap<RegisterViewModel, ApplicationUserProfile>()
            .ForMember(profile => profile.Id, expression => expression.Ignore())
            .ForMember(profile => profile.Permissions, expression => expression.Ignore())
            .ForMember(profile => profile.ApplicationUser, expression => expression.Ignore())
            .ForMember(profile => profile.CreatedAt, expression => expression.Ignore())
            .ForMember(profile => profile.CreatedBy, expression => expression.Ignore())
            .ForMember(profile => profile.UpdatedAt, expression => expression.Ignore())
            .ForMember(profile => profile.UpdatedBy, expression => expression.Ignore());

        CreateMap<ClaimsIdentity, UserProfileViewModel>()
            .ForMember(profileViewModel => profileViewModel.Id,
                expression => expression.MapFrom(claims => ClaimsHelper.GetValue<Guid>(claims, ClaimTypes.NameIdentifier)))
            .ForMember(profileViewModel => profileViewModel.PositionName,
                expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Actor)))
            .ForMember(profileViewModel => profileViewModel.FirstName,
                expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.GivenName)))
            .ForMember(profileViewModel => profileViewModel.LastName,
                expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Surname)))
            .ForMember(profileViewModel => profileViewModel.Roles,
                expression => expression.MapFrom(claims => ClaimsHelper.GetValues<string>(claims, ClaimTypes.Role)))
            .ForMember(profileViewModel => profileViewModel.Email,
                expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Name)))
            .ForMember(profileViewModel => profileViewModel.PhoneNumber,
                expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.MobilePhone)));
    }
}