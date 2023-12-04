using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class AnamnesisAnswerModelConfiguration : IdentityModelConfigurationBase<AnamnesisAnswer>
{
    protected override void AddBuilder(EntityTypeBuilder<AnamnesisAnswer> builder)
    {
        builder.HasOne(anamnesisAnswer => anamnesisAnswer.Question);
        builder.HasOne(anamnesisAnswer => anamnesisAnswer.Anamnesis);
        builder.HasMany(anamnesisAnswer => anamnesisAnswer.Answers);
    }

    protected override string GetTableName() => nameof(AnamnesisAnswer);
}