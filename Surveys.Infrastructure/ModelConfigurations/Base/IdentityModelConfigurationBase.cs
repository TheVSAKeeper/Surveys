using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class IdentityModelConfigurationBase<T> : ModelConfigurationBase<T> where T : Identity
{
    protected sealed override void AddBaseConfiguration(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        AddIdentityConfiguration(builder);
    }

    protected abstract void AddIdentityConfiguration(EntityTypeBuilder<T> builder);
}