using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class QuestionModelConfiguration : IdentityModelConfigurationBase<Question>
{
    protected override void AddIdentityConfiguration(EntityTypeBuilder<Question> builder)
    {
        builder.Property(question => question.Content)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(question => question.SortIndex).IsRequired();

        builder.Property(question => question.SortIndex).IsRequired();

        builder.HasOne(question => question.AnamnesisTemplate);

        builder.HasMany(question => question.AnamnesisAnswers);
    }
}