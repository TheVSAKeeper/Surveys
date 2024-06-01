namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class SortableIdentityModelConfigurationBase<T> : IdentityModelConfigurationBase<T> where T : SortableIdentity
{
    protected override void AddBaseConfiguration(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.SortIndex);

        AddCustomConfiguration(builder);
    }

    protected abstract override void AddCustomConfiguration(EntityTypeBuilder<T> builder);
}