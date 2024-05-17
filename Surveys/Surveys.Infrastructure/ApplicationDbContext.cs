using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Surveys.Domain;
using Surveys.Infrastructure.Base;

namespace Surveys.Infrastructure;

/// <summary>
///     Database context for current application
/// </summary>
public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Anamnesis> Anamneses { get; set; } = null!;
    public DbSet<AnamnesisAnswer> AnamnesisAnswers { get; set; } = null!;
    public DbSet<AnamnesisTemplate> AnamnesisTemplates { get; set; } = null!;

    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;

    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<SurveyDiagnosis> SurveyDiagnoses { get; set; } = null!;

    public DbSet<Survey> Surveys { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;

    public DbSet<EventItem> EventItems { get; set; }

    public DbSet<ApplicationUserProfile> Profiles { get; set; }

    public DbSet<AppPermission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseOpenIddict<Guid>();
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // It should be removed when using real Database (not in memory mode)
        // optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        base.OnConfiguring(optionsBuilder);
    }
}

/// <summary>
///     ATTENTION!
///     It should uncomment two line below when using real Database (not in memory mode). Don't forget update connection
///     string.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=surveys_test;Username=postgres;");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}