namespace Surveys.Infrastructure.ModelConfigurations;

public class QuestionModelConfiguration : SortableIdentityModelConfigurationBase<Question>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<Question> builder)
    {
        builder.Property(question => question.Text)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(question => question.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.HasMany(question => question.Options)
            .WithOne(option => option.Question)
            .HasForeignKey(question => question.QuestionId);

        builder.Navigation(question => question.Options).AutoInclude();
    }
}
