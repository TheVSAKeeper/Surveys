using System.Reflection;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Surveys.Domain.Base;

namespace Surveys.Infrastructure.Base;

public abstract class DbContextBase(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    private const string DefaultUserName = "Anonymous";
    private const string CreatedAt = "CreatedAt";
    private const string CreatedBy = "CreatedBy";
    private const string UpdatedAt = "UpdatedAt";
    private const string UpdatedBy = "UpdatedBy";

    public SaveChangesResult LastSaveChangesResult { get; } = new();

    public override int SaveChanges() => SaveChanges(true);

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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) => SaveChangesAsync(true, cancellationToken);

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

    private void DbSaveChanges()
    {
        SaveCreated();
        SaveUpdated();
    }

    private void SaveCreated()
    {
        IEnumerable<EntityEntry> createdEntries = ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Added);

        foreach (EntityEntry entry in createdEntries)
        {
            if (entry.Entity is IAuditable)
            {
                object userName = entry.Property(CreatedBy).CurrentValue ?? DefaultUserName;
                DateTime creationDate = DateTime.Now.ToUniversalTime();

                entry.Property(CreatedAt).CurrentValue ??= creationDate;
                entry.Property(UpdatedAt).CurrentValue ??= creationDate;

                entry.Property(CreatedBy).CurrentValue = userName;
                entry.Property(UpdatedBy).CurrentValue = userName;
            }

            LastSaveChangesResult.AddMessage($"ChangeTracker has new entities: {entry.Entity.GetType()}");
        }
    }

    private void SaveUpdated()
    {
        IEnumerable<EntityEntry> updatedEntries = ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (EntityEntry entry in updatedEntries)
        {
            if (entry.Entity is IAuditable)
            {
                entry.Property(UpdatedBy).CurrentValue ??= DefaultUserName;
                entry.Property(UpdatedAt).CurrentValue = DateTime.Now.ToUniversalTime();
            }

            LastSaveChangesResult.AddMessage($"ChangeTracker has modified entities: {entry.Entity.GetType()}");
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}