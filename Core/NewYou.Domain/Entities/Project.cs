using NewYou.Domain.Entities.Common;

namespace NewYou.Domain.Entities;
public class Project : BaseEntity
{
    public string Name { get; set; }
    public string ColorHex { get; set; }

    public Guid OwnerId { get; set; } 
    public Account Owner { get; set; }

    public ICollection<ToDoItem> ToDoItems { get; set; }
}