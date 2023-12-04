using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class AnamnesisModelConfiguration : IdentityModelConfigurationBase<Anamnesis>
{
    protected override void AddBuilder(EntityTypeBuilder<Anamnesis> builder)
    {
        builder.HasOne(anamnesis => anamnesis.Survey);
        builder.HasMany(anamnesis => anamnesis.Answers);
    }

    protected override string GetTableName() => nameof(Anamnesis);
}