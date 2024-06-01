namespace Surveys.Infrastructure.ModelConfigurations;

public class AnamnesisModelConfiguration : SortableAuditableModelConfigurationBase<Anamnesis>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<Anamnesis> builder)
    {
        builder.HasOne(anamnesis => anamnesis.Survey)
            .WithMany(survey => survey.Anamneses)
            .HasForeignKey(anamnesis => anamnesis.SurveyId);

        builder.HasOne(anamnesis => anamnesis.AnamnesisTemplate)
            .WithMany(anamnesisTemplate => anamnesisTemplate.Anamneses)
            .HasForeignKey(anamnesis => anamnesis.AnamnesisTemplateId);

        builder.Property(anamnesis => anamnesis.IsComplete);
    }
}