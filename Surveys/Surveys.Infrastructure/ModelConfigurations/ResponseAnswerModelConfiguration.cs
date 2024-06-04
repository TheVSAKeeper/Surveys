namespace Surveys.Infrastructure.ModelConfigurations;

public class ResponseAnswerModelConfiguration : IdentityModelConfigurationBase<ResponseAnswer>
{
    protected override void AddCustomConfiguration(EntityTypeBuilder<ResponseAnswer> builder)
    {
        builder.Property(responseAnswer => responseAnswer.Value)
            .IsRequired()
            .HasMaxLength(1024);

        builder.HasOne(response => response.Response)
            .WithMany(anamnesis => anamnesis.Answers)
            .HasForeignKey(response => response.ResponseId);
    }
}