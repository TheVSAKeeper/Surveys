using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class SurveyDiagnosisModelConfiguration : AuditableModelConfigurationBase<SurveyDiagnosis>
{
    protected override void AddBuilder(EntityTypeBuilder<SurveyDiagnosis> builder)
    {
        builder.Property(surveyDiagnosis => surveyDiagnosis.Reason)
               .HasMaxLength(1024)
               .IsRequired();

        builder.HasOne(surveyDiagnosis => surveyDiagnosis.Diagnosis);
        builder.HasOne(surveyDiagnosis => surveyDiagnosis.Patient);
        builder.HasOne(surveyDiagnosis => surveyDiagnosis.Survey);
    }

    protected override string GetTableName() => nameof(SurveyDiagnosis);
}