using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class AnamnesisModelConfiguration : AuditableModelConfigurationBase<Anamnesis>
{
    protected override void AddConfiguration(EntityTypeBuilder<Anamnesis> builder)
    {
        builder.Property(survey => survey.IsComplete)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(anamnesis => anamnesis.AnamnesisTemplate);
        builder.Navigation(anamnesis => anamnesis.AnamnesisTemplate).AutoInclude();

        builder.HasMany(anamnesis => anamnesis.AnamnesisAnswers);
        builder.Navigation(anamnesis => anamnesis.AnamnesisAnswers).AutoInclude();

        builder.HasOne(anamnesis => anamnesis.Survey);
    }

    protected override string GetTableName() => nameof(Anamnesis);
}