using System.Linq.Expressions;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class AuditableModelConfigurationBase<T> : IdentityModelConfigurationBase<T> where T : Auditable
{
    protected override void AddBaseConfiguration(EntityTypeBuilder<T> builder)
    {
        Expression<Func<DateTime, DateTime>> convertToUtc = dateTime =>
            dateTime.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

        builder.Property(x => x.CreatedAt)
            .HasConversion(convertToUtc, convertToUtc)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasConversion(dateTime => dateTime!.Value.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc),
                dateTime => dateTime!.Value.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc));

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(256);

        AddCustomConfiguration(builder);
    }

    protected abstract override void AddCustomConfiguration(EntityTypeBuilder<T> builder);
}