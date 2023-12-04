using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

/// <summary>
///     Базовая конфигурация для модели с возможностью аудита
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class IdentityModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Identity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(GetTableName());
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        AddBuilder(builder);
    }

    /// <summary>
    ///     Добавьте настраиваемые свойства для вашего объекта
    /// </summary>
    /// <param name="builder"></param>
    protected abstract void AddBuilder(EntityTypeBuilder<T> builder);

    /// <summary>
    ///     Название таблицы
    /// </summary>
    /// <returns></returns>
    protected abstract string GetTableName();
}