using Microsoft.EntityFrameworkCore;

namespace ConsoleTest;

internal class ApplicationDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=postgres;");
    }
}

internal class Program
{
    public static void Main()
    {
        using ApplicationDbContext context = InitializeDb();

        DbSet<Question> questions = context.Questions;

        Console.WriteLine();
        Console.ReadLine();
    }

    private static ApplicationDbContext InitializeDb()
    {
        ApplicationDbContext context = new();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
            context.Database.Migrate();

        context.Questions.AddRange(new NumericQuestion
            {
                Content = 1,
                Text = "NumericQuestion"
            },
            new TextQuestion
            {
                Content = "Test",
                Text = "TextQuestion"
            },
            new VariantsQuestion
            {
                Content = ["Variant1", "Variant2"],
                Text = "VariantsQuestion"
            });

        context.SaveChanges();

        return context;
    }
}

public abstract class Identity
{
    public Guid Id { get; set; }
}

internal class Question : Identity
{
    public virtual required string Text { get; set; }
}

internal class NumericQuestion : Question, IContentHaving<int>
{
    
    public required int Content { get; set; }
}

internal class TextQuestion : Question, IContentHaving<string>
{
    public required string Content { get; set; }
}

internal class VariantsQuestion : Question, IContentHaving<IList<string>>
{
    public required IList<string> Content { get; set; }
}

internal interface IContentHaving<T>
{
    public T Content { get; set; }
}