using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewYou.Domain.Entities;
using NewYou.Domain.Entities.Common;
using System.Reflection;
namespace NewYou.Persistance.Contexts;
public class NewYouDbContext :IdentityDbContext<Account , IdentityRole<Guid>,Guid>
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public NewYouDbContext(DbContextOptions<NewYouDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateData = DateTime.UtcNow.AddHours(4);
                    entry.Entity.UpdateData = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdateData = DateTime.UtcNow.AddHours(4);
                    break;

                    //case EntityState.Deleted:
                    //    entry.State = EntityState.Modified; // Soft delete
                    //    entry.Entity.IsDeleted = true;
                    //    entry.Entity.RemoveData = DateTime.UtcNow.AddHours(4);
                    //    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}

