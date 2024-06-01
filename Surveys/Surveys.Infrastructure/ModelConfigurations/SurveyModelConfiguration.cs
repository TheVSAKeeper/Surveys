namespace Surveys.Infrastructure.ModelConfigurations;

public class SurveyModelConfiguration : AuditableModelConfigurationBase<Survey>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<Survey> builder)
    {
        builder.Property(survey => survey.Complaint)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(survey => survey.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(survey => survey.Patient)
            .WithMany(patient => patient.Surveys)
            .HasForeignKey(survey => survey.PatientId);

        builder.Navigation(survey => survey.Patient).AutoInclude();
        builder.Navigation(survey => survey.Anamneses).AutoInclude();
    }
}