using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public DbSet<TaskGroup> TaskGroups { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        if (entry.Entity.IsDeleted)
                        {
                            entry.Entity.Deleted = DateTime.UtcNow;
                        }
                        else
                        {
                            entry.Entity.LastModified = DateTime.UtcNow;
                        }
                        break;
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        break;                    
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
