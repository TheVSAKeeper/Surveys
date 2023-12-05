using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

public class PatientModelConfiguration : IdentityModelConfigurationBase<Patient>
{
    protected override void AddBuilder(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(patient => patient.FirstName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(patient => patient.LastName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(patient => patient.Patronymic).HasMaxLength(50);
        builder.Property(patient => patient.BirthDate).IsRequired();

        builder.HasMany(patient => patient.Surveys);
        builder.HasMany(patient => patient.SurveyDiagnoses);
    }

    protected override string GetTableName() => nameof(Patient);
}