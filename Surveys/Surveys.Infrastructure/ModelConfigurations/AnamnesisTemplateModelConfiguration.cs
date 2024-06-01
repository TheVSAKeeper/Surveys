namespace Surveys.Infrastructure.ModelConfigurations;

public class AnamnesisTemplateModelConfiguration : SortableIdentityModelConfigurationBase<AnamnesisTemplate>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<AnamnesisTemplate> builder)
    {
        builder.Property(anamnesisTemplate => anamnesisTemplate.Title)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(anamnesisTemplate => anamnesisTemplate.Description)
            .HasMaxLength(1024);

        builder.HasMany(anamnesisTemplate => anamnesisTemplate.Questions)
            .WithOne(question => question.AnamnesisTemplate)
            .HasForeignKey(question => question.AnamnesisTemplateId)
            .IsRequired();
    }
}