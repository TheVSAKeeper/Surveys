using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class DiagnosisModelConfiguration : IdentityModelConfigurationBase<Diagnosis>
{
    protected override void AddBuilder(EntityTypeBuilder<Diagnosis> builder)
    {
        builder.Property(diagnosis => diagnosis.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(diagnosis => diagnosis.Description).HasMaxLength(1024);

        builder.HasMany(diagnosis => diagnosis.SurveyDiagnoses);
    }

    protected override string GetTableName() => nameof(Diagnosis);
}