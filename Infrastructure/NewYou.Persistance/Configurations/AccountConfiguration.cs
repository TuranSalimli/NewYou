using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewYou.Domain.Entities;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(a => a.LastName).HasMaxLength(50).IsRequired();

        builder.HasMany(a => a.ToDoItems)
               .WithOne(t => t.Owner)
               .HasForeignKey(t => t.OwnerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}