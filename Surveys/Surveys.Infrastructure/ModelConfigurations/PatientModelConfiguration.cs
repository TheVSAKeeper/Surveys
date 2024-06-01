namespace Surveys.Infrastructure.ModelConfigurations;

public class PatientModelConfiguration : IdentityModelConfigurationBase<Patient>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(patient => patient.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(patient => patient.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(patient => patient.Patronymic)
            .HasMaxLength(50);

        builder.Property(patient => patient.BirthDate)
            .IsRequired();
    }
}