using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class AuditableModelConfigurationBase<T> : ModelConfigurationBase<T> where T : Auditable
{
    protected override void AddBaseConfiguration(EntityTypeBuilder<T> builder)
    {
      builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion(time => time, value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasConversion(time => time!.Value, value => DateTime.SpecifyKind(value, DateTimeKind.Utc));

        builder.Property(x => x.UpdatedBy).HasMaxLength(256);

        AddAuditableConfiguration(builder);
    }

    protected abstract void AddAuditableConfiguration(EntityTypeBuilder<T> builder);
}