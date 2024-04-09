using AutoMapper;
using Surveys.Infrastructure;
using Surveys.Web.Application.Messaging.ProfileMessages.ViewModels;
using Calabonga.Microservices.Core;
using System.Security.Claims;

namespace Surveys.Web.Application.Messaging.ProfileMessages;

/// <summary>
/// Mapper Configuration for entity ApplicationUser
/// </summary>
public sealed class ProfilesMapperConfiguration : Profile
{
    /// <inheritdoc />
    public ProfilesMapperConfiguration()
    {
        CreateMap<RegisterViewModel, ApplicationUser>()
            .ForMember(user => user.UserName, expression => expression.MapFrom(p => p.Email))
            .ForMember(user => user.Email, expression => expression.MapFrom(p => p.Email))
            .ForMember(user => user.EmailConfirmed, expression => expression.MapFrom(src => true))
            .ForMember(user => user.FirstName, expression => expression.MapFrom(p => p.FirstName))
            .ForMember(user => user.LastName, expression => expression.MapFrom(p => p.LastName))
            .ForMember(user => user.Patronymic, expression => expression.MapFrom(p => p.Patronymic))
            .ForMember(user => user.DisplayName, expression => expression.MapFrom(p => p.Email))
            .ForMember(user => user.Roles, expression => expression.Ignore())
            .ForMember(user => user.PhoneNumberConfirmed, expression => expression.MapFrom(src => true))
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
            .ForMember(user => user.Id, expression => expression.Ignore())
            .ForMember(user => user.Permissions, expression => expression.Ignore())
            .ForMember(user => user.ApplicationUser, expression => expression.Ignore())
            .ForMember(user => user.CreatedAt, expression => expression.Ignore())
            .ForMember(user => user.CreatedBy, expression => expression.Ignore())
            .ForMember(user => user.UpdatedAt, expression => expression.Ignore())
            .ForMember(user => user.UpdatedBy, expression => expression.Ignore());

        CreateMap<ClaimsIdentity, UserProfileViewModel>()
            .ForMember(user => user.Id, expression => expression.MapFrom(claims => ClaimsHelper.GetValue<Guid>(claims, ClaimTypes.NameIdentifier)))
            .ForMember(user => user.PositionName, expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Actor)))
            .ForMember(user => user.FirstName, expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.GivenName)))
            .ForMember(user => user.LastName, expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Surname)))
            .ForMember(user => user.Roles, expression => expression.MapFrom(claims => ClaimsHelper.GetValues<string>(claims, ClaimTypes.Role)))
            .ForMember(user => user.Email, expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Name)))
            .ForMember(user => user.PhoneNumber, expression => expression.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.MobilePhone)));
    }
}