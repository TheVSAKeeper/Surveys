﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
             using Surveys.Infrastructure.ModelConfigurations.Base;
             namespace Surveys.Infrastructure.ModelConfigurations;
             /// <summary>
             ///     Конфигурация типа сущности для сущности Person
             /// </summary>
             public class ApplicationUserProfileModelConfiguration : AuditableModelConfigurationBase<ApplicationUserProfile>
             {
                 protected override void AddBuilder(EntityTypeBuilder<ApplicationUserProfile> builder)
                 {
                     builder.HasOne(x => x.ApplicationUser);
                 }
                 protected override string TableName() => "Profiles";
             }