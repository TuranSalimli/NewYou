using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewYou.Domain.Entities;

namespace NewYou.Persistance.Configurations;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(t => t.Note)
               .HasMaxLength(1000);

        builder.HasQueryFilter(t => !t.IsDeleted);

        builder.HasOne(t => t.Project)
               .WithMany(p => p.ToDoItems)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}