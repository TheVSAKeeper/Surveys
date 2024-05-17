using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class IdentityModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Identity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(GetTableName());

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        AddConfiguration(builder);
    }

    protected abstract void AddConfiguration(EntityTypeBuilder<T> builder);

    protected abstract string GetTableName();
}