using NewYou.Domain.Entities.Common;
using NewYou.Domain.Enums;
namespace NewYou.Domain.Entities;
public class ToDoItem : BaseEntity
{
    public string Title { get; set; }
    public string Note { get; set; }
    public ToDoItemPriority Priority { get; set; } 
    public ToDoItemStatus Status { get; set; } 
    public DateTime? Deadline { get; set; } 
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public Guid OwnerId { get; set; }
    public Account Owner { get; set; }
}