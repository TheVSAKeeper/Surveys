using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Surveys.Infrastructure.Base;

namespace Surveys.Infrastructure;

/// <summary>
///     Database context for current application
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContextBase(options)
{
    public DbSet<Anamnesis> Anamneses { get; set; }
    public DbSet<AnamnesisTemplate> AnamnesisTemplates { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<EventItem> EventItems { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<ResponseAnswer> ResponseAnswers { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<SurveyDiagnosis> SurveyDiagnoses { get; set; }

    public DbSet<ApplicationUserProfile> Profiles { get; set; }
    public DbSet<AppPermission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseOpenIddict<Guid>();
        base.OnModelCreating(builder);
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=surveys_test;Username=postgres;");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}