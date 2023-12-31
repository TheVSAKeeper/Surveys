﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Surveys.WPF.Definitions.Base;
using Surveys.WPF.Pages.Home;
using Surveys.WPF.Shared.Navigation;

namespace Surveys.WPF.Definitions.ViewModels;

public class HomeDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<HomeViewModel>();

        services.AddNavigationService<HomeViewModel>();
    }
}