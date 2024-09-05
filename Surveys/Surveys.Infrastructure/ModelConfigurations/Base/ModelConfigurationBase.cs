using Microsoft.EntityFrameworkCore;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class ModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(GetTableName());

        AddBaseConfiguration(builder);
    }

    protected virtual string GetTableName()
    {
        return typeof(T).Name;
    }

    protected abstract void AddBaseConfiguration(EntityTypeBuilder<T> builder);
}
