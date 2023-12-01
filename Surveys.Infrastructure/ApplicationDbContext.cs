using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Surveys.Domain;
using Surveys.Infrastructure.Base;

namespace Surveys.Infrastructure;

public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<EventItem> EventItems { get; set; }

    public DbSet<ApplicationUserProfile> Profiles { get; set; }

    public DbSet<AppPermission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.UseOpenIddict<Guid>();
        base.OnModelCreating(builder);
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=surveys_test;Username=postgres;)");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}