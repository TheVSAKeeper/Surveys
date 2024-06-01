namespace Surveys.Infrastructure.ModelConfigurations;

public class DiagnosisModelConfiguration : IdentityModelConfigurationBase<Diagnosis>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<Diagnosis> builder)
    {
        builder.Property(diagnosis => diagnosis.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(diagnosis => diagnosis.Description)
            .HasMaxLength(1024);
    }
}