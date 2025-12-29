using NewYou.Domain.Entities.Common;

namespace NewYou.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid ActorId { get; set; } 
    public string EntityName { get; set; }
    public Guid EntityId { get; set; }
    public string OperationType { get; set; } 
    public string TraceData { get; set; }
}