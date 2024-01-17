using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class AnamnesisTemplateModelConfiguration : IdentityModelConfigurationBase<AnamnesisTemplate>
{
    protected override void AddConfiguration(EntityTypeBuilder<AnamnesisTemplate> builder)
    {
        builder.Property(anamnesisTemplate => anamnesisTemplate.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(anamnesisTemplate => anamnesisTemplate.SortIndex).IsRequired();

        builder.Property(anamnesisTemplate => anamnesisTemplate.SortIndex).IsRequired();

        builder.HasMany(anamnesisTemplate => anamnesisTemplate.Anamneses);
        builder.HasMany(anamnesisTemplate => anamnesisTemplate.Questions);
    }

    protected override string GetTableName() => nameof(AnamnesisTemplate);
}