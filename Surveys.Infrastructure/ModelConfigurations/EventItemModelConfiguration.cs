using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain;
using Surveys.Infrastructure.ModelConfigurations.Base;

namespace Surveys.Infrastructure.ModelConfigurations;

/// <summary>
///     Конфигурация типа сущности для сущности Log
/// </summary>
public class EventItemModelConfiguration : IdentityModelConfigurationBase<EventItem>
{
    protected override void AddBuilder(EntityTypeBuilder<EventItem> builder)
    {
        builder.Property(x => x.Logger)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(x => x.Level)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.Message)
               .HasMaxLength(4000)
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
               .IsRequired();

        builder.Property(x => x.ThreadId).HasMaxLength(255);
        builder.Property(x => x.ExceptionMessage).HasMaxLength(2000);
    }

    protected override string GetTableName() => nameof(EventItem);
}