using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewYou.Domain.Entities;

namespace NewYou.Persistance.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ActorId)
               .IsRequired();

        builder.Property(x => x.EntityName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.EntityId)
               .IsRequired();

        builder.Property(x => x.OperationType)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.TraceData)
               .IsRequired(false);

        builder.HasQueryFilter(x => true);

        // Cədvəl adını bazada fərqli görmək istəsən:
        builder.ToTable("AuditLogs");
    }
}