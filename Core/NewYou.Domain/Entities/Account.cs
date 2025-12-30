using Microsoft.AspNetCore.Identity;
using NewYou.Domain.Enums;

namespace NewYou.Domain.Entities;
public class Account : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    public string? ImgUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginDate { get; set; }
    public bool IsArchived { get; set; } = false;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

    public ICollection<ToDoItem> ToDoItems { get; set; } = new HashSet<ToDoItem>();
    
    public ICollection<AuditLog> AuditLogs { get; set; } = new HashSet<AuditLog>();
}