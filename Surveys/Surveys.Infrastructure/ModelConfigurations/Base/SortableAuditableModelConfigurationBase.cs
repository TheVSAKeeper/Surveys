namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class SortableAuditableModelConfigurationBase<T> : AuditableModelConfigurationBase<T> where T : SortableAuditable
{
    protected override void AddBaseConfiguration(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.SortIndex);

        AddCustomConfiguration(builder);
    }

    protected abstract override void AddCustomConfiguration(EntityTypeBuilder<T> builder);
}