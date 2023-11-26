using System.Reflection;
using Surveys.Domain.Base;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Surveys.Infrastructure;

namespace Surveys.Infrastructure.Base;

public abstract class DbContextBase : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private const string DefaultUserName = "Anonymous";

    protected DbContextBase(DbContextOptions options) : base(options)
    {
        LastSaveChangesResult = new SaveChangesResult();
    }

    public SaveChangesResult LastSaveChangesResult { get; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        try
        {
            DbSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return Task.FromResult(0);
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        try
        {
            DbSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return 0;
        }
    }

    public override int SaveChanges()
    {
        try
        {
            DbSaveChanges();
            return base.SaveChanges();
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return 0;
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        try
        {
            DbSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return Task.FromResult(0);
        }
    }

    private void DbSaveChanges()
    {
        const string CreatedAt = "CreatedAt";
        const string CreatedBy = "CreatedBy";
        const string UpdatedAt = "UpdatedAt";
        const string UpdatedBy = "UpdatedBy";

        IEnumerable<EntityEntry> createdEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

        foreach (EntityEntry entry in createdEntries)
        {
            if (entry.Entity is not IAuditable)
                continue;

            DateTime creationDate = DateTime.Now.ToUniversalTime();

            object? userName = entry.Property(CreatedBy).CurrentValue == null
                ? DefaultUserName
                : entry.Property(CreatedBy).CurrentValue;

            object? updatedAt = entry.Property(UpdatedAt).CurrentValue;
            object? createdAt = entry.Property(CreatedAt).CurrentValue;

            if (createdAt != null)
            {
                if (DateTime.Parse(createdAt.ToString()).Year > 1970)
                    entry.Property(CreatedAt).CurrentValue = ((DateTime)createdAt).ToUniversalTime();
                else
                    entry.Property(CreatedAt).CurrentValue = creationDate;
            }
            else
            {
                entry.Property(CreatedAt).CurrentValue = creationDate;
            }

            if (updatedAt != null)
            {
                if (DateTime.Parse(updatedAt.ToString()).Year > 1970)
                    entry.Property(UpdatedAt).CurrentValue = ((DateTime)updatedAt).ToUniversalTime();
                else
                    entry.Property(UpdatedAt).CurrentValue = creationDate;
            }
            else
            {
                entry.Property(UpdatedAt).CurrentValue = creationDate;
            }

            entry.Property(CreatedBy).CurrentValue = userName;
            entry.Property(UpdatedBy).CurrentValue = userName;

            LastSaveChangesResult.AddMessage($"ChangeTracker has new entities: {entry.Entity.GetType()}");
        }

        IEnumerable<EntityEntry> updatedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

        foreach (EntityEntry entry in updatedEntries)
        {
            if (entry.Entity is IAuditable)
            {
                object? userName = entry.Property(UpdatedBy).CurrentValue == null
                    ? DefaultUserName
                    : entry.Property(UpdatedBy).CurrentValue;

                entry.Property(UpdatedAt).CurrentValue = DateTime.Now.ToUniversalTime();
                entry.Property(UpdatedBy).CurrentValue = userName;
            }

            LastSaveChangesResult.AddMessage($"ChangeTracker has modified entities: {entry.Entity.GetType()}");
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

        MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(x => x.Name == "ApplyConfiguration");

        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
        {
            foreach (Type item in type.GetInterfaces())
            {
                if (!item.IsConstructedGenericType || item.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>))
                    continue;

                MethodInfo applyConcreteMethod = applyGenericMethod.MakeGenericMethod(item.GenericTypeArguments[0]);
                applyConcreteMethod.Invoke(builder, new[] { Activator.CreateInstance(type) });
                break;
            }
        }
    }
}