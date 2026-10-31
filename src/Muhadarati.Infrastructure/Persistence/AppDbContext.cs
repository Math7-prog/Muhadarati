using Microsoft.EntityFrameworkCore;
using Muhadarati.Application.Common.Interfaces;
using Muhadarati.Domain.Academic;
using Muhadarati.Domain.Common;

namespace Muhadarati.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<College> Colleges => Set<College>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Stage> Stages => Set<Stage>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<SubjectStageLink> SubjectStageLinks => Set<SubjectStageLink>();
    public DbSet<Lecture> Lectures => Set<Lecture>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
                entry.Entity.UpdatedAt = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = utcNow;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SubjectStageLink>())
        {
            if (entry.State == EntityState.Added && entry.Entity.CreatedAt == default)
            {
                entry.Entity.CreatedAt = utcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
