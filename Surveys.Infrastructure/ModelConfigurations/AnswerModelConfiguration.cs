﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class AnswerModelConfiguration : IdentityModelConfigurationBase<Answer>
{
    protected override void AddConfiguration(EntityTypeBuilder<Answer> builder)
    {
        builder.Property(answer => answer.Content)
            .HasMaxLength(1024)
            .IsRequired();

        builder.HasOne(answer => answer.AnamnesisAnswers);
    }

    protected override string GetTableName() => nameof(Answer);
}