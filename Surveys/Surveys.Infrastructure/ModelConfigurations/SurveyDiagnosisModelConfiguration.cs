namespace Surveys.Infrastructure.ModelConfigurations;

public class SurveyDiagnosisModelConfiguration : AuditableModelConfigurationBase<SurveyDiagnosis>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<SurveyDiagnosis> builder)
    {
        builder.Property(surveyDiagnosis => surveyDiagnosis.Reason)
            .HasMaxLength(1024)
            .IsRequired();

        builder.HasOne(surveyDiagnosis => surveyDiagnosis.Diagnosis)
            .WithMany(diagnosis => diagnosis.SurveyDiagnoses)
            .HasForeignKey(surveyDiagnosis => surveyDiagnosis.DiagnosisId);

        builder.HasOne(surveyDiagnosis => surveyDiagnosis.Patient)
            .WithMany(patient => patient.SurveyDiagnoses)
            .HasForeignKey(surveyDiagnosis => surveyDiagnosis.PatientId);

        builder.HasOne(surveyDiagnosis => surveyDiagnosis.Survey)
            .WithMany(survey => survey.SurveyDiagnoses)
            .HasForeignKey(surveyDiagnosis => surveyDiagnosis.SurveyId);
    }
}
