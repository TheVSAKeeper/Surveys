using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Surveys.Infrastructure.ModelConfigurations;

public class ApplicationUserModelConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(user => user.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(user => user.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(user => user.Patronymic).HasMaxLength(50);
        builder.Property(user => user.DisplayName).HasMaxLength(50);

        builder.Navigation(user => user.Roles).AutoInclude();

        builder.HasMany(user => user.Roles);
    }
}