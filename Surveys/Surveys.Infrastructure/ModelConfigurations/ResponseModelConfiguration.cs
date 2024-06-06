namespace Surveys.Infrastructure.ModelConfigurations;

public class ResponseModelConfiguration : AuditableModelConfigurationBase<Response>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<Response> builder)
    {
        builder.HasOne(response => response.Anamnesis)
            .WithMany(anamnesis => anamnesis.Responses)
            .HasForeignKey(response => response.AnamnesisId);

        builder.HasOne(response => response.Question)
            .WithMany(anamnesis => anamnesis.Answers)
            .HasForeignKey(response => response.QuestionId);

        builder.Navigation(question => question.Question).AutoInclude();
        builder.Navigation(question => question.Answers).AutoInclude();
    }
}