using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

/// <summary>
///     Базовая конфигурация модели с возможностью аудита
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AuditableModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Auditable
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(TableName());
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        // аудит
        builder.Property(x => x.CreatedAt).IsRequired().HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)).IsRequired();
        builder.Property(x => x.CreatedBy).HasMaxLength(256).IsRequired();
        builder.Property(x => x.UpdatedAt).HasConversion(v => v.Value, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(x => x.UpdatedBy).HasMaxLength(256);
        AddBuilder(builder);
    }

    /// <summary>
    ///     Добавить пользовательские свойства для вашей сущности
    /// </summary>
    /// <param name="builder"></param>
    protected abstract void AddBuilder(EntityTypeBuilder<T> builder);

    /// <summary>
    ///     Имя таблицы
    /// </summary>
    /// <returns></returns>
    protected abstract string TableName();
}