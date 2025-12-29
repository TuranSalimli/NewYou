using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewYou.Domain.Entities;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.ColorHex)
               .HasMaxLength(7)
               .HasDefaultValue("#5733FF");

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}