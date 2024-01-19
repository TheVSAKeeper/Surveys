using Microsoft.EntityFrameworkCore;

namespace ConsoleTest;

internal class ApplicationDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;

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
        DbSet<Answer> answers = context.Answers;

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
            } /*,
            new VariantsQuestion
            {
                Content = ["Variant1", "Variant2"],
                Text = "VariantsQuestion"
            }*/
        );

        context.SaveChanges();

        AnswerFactory answerFactory = new();

        foreach (Question question in context.Questions)
        {
            Answer entity = answerFactory.Create(question, "1");
            context.Answers.Add(entity);
        }

        context.SaveChanges();

        return context;
    }
}

internal abstract class Identity
{
    public Guid Id { get; set; }
}

internal class AnamnesisAnswer : Identity
{
    public required Guid QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;

    public virtual IList<Answer> Answers { get; set; } = null!;
}

internal class Answer : Identity
{
    public Answer(string text, Question question)
    {
        Text = text;
        Question = question;
    }

    public Answer()
    {
    }

    public string Text { get; private set; }

    public Question Question { get; private set; }
}

internal class ParameterAnswer<T> : Answer
{
    public ParameterAnswer()
    {
    }

    public ParameterAnswer(T content, Question question) : base(content.ToString(), question)
    {
        Content = content;
    }

    public T Content { get; private set; }
}

internal enum ContentType
{
    None,
    Text,

    //  Selection,
    Numeric
    //  Date
}

internal class AnswerFactory
{
    public Answer Create(Question question, object content)
    {
        ContentType type = question.AnswerType;

        return type switch
        {
            ContentType.None => new Answer(content as string ?? throw new InvalidOperationException(), question),
            ContentType.Text => new ParameterAnswer<string>(content as string ?? throw new InvalidOperationException(), question),
            ContentType.Numeric => new ParameterAnswer<int>(Convert.ToInt32(content), question),
            var _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}

internal class Question : Identity
{
    public virtual ContentType AnswerType { get; set; }
    public virtual required string Text { get; set; }
}

internal class NumericQuestion : Question
{
    public required int Content { get; set; }

    public override ContentType AnswerType { get; set; } = ContentType.Numeric;
}

internal class TextQuestion : Question
{
    public required string Content { get; set; }

    public override ContentType AnswerType { get; set; } = ContentType.Text;
}

internal class VariantsQuestion : Question
{
    public required IList<string> Content { get; set; }
}

internal interface IContentHaving<T>
{
    public T Content { get; set; }
}