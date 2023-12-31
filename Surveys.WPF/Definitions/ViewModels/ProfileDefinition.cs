﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Pages.Profile;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Definitions.ViewModels;

public class ProfileDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<ProfileViewModel>();

        services.AddNavigationService<ProfileViewModel>();
    }
}