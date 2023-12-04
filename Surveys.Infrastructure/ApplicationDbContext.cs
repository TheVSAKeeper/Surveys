using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Surveys.Domain;
using Surveys.Infrastructure.Base;

namespace Surveys.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContextBase(options)
{
    public DbSet<Anamnesis> Anamneses { get; set; } = null!;
    public DbSet<AnamnesisAnswer> AnamnesisAnswers { get; set; } = null!;
    public DbSet<AnamnesisTemplate> AnamnesisTemplates { get; set; } = null!;

    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;

    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<SurveyDiagnosis> SurveyDiagnoses { get; set; } = null!;

    public DbSet<Survey> Surveys { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;

    public DbSet<EventItem> EventItems { get; set; } = null!;
    public DbSet<ApplicationUserProfile> Profiles { get; set; } = null!;
    public DbSet<AppPermission> Permissions { get; set; } = null!;
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