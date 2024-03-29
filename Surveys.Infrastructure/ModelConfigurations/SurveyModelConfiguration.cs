﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class SurveyModelConfiguration : AuditableModelConfigurationBase<Survey>
{
    protected override void AddAuditableConfiguration(EntityTypeBuilder<Survey> builder)
    {
        builder.Property(survey => survey.Complaint)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(survey => survey.IsComplete)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(survey => survey.Patient);
        builder.HasMany(survey => survey.SurveyDiagnoses);
        builder.HasMany(survey => survey.Anamneses);
    }
}