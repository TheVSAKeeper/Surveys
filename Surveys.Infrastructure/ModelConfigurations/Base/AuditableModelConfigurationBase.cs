﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.ModelConfigurations.Base;

public abstract class AuditableModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Auditable
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(GetTableName());

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasConversion(v => v!.Value, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(x => x.UpdatedBy).HasMaxLength(256);

        AddConfiguration(builder);
    }
    
    protected abstract void AddConfiguration(EntityTypeBuilder<T> builder);
    
    protected abstract string GetTableName();
}