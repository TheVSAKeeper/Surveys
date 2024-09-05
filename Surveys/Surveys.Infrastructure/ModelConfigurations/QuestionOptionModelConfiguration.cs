namespace Surveys.Infrastructure.ModelConfigurations;

public class QuestionOptionModelConfiguration : SortableIdentityModelConfigurationBase<QuestionOption>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.Property(option => option.Value)
            .IsRequired()
            .HasMaxLength(1024);
    }
}
