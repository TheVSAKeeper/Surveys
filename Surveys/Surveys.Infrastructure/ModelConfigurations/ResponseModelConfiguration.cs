namespace Surveys.Infrastructure.ModelConfigurations;

public class ResponseModelConfiguration : AuditableModelConfigurationBase<Response>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<Response> builder)
    {
        builder.HasOne(response => response.Anamnesis)
            .WithMany(anamnesis => anamnesis.Responses)
            .HasForeignKey(response => response.AnamnesisId);
    }
}