using NewYou.Application.GenericResponses; 
using NewYou.Domain.Enums;

namespace NewYou.Application.Features.Commands.ToDoItem.CreateToDo;
public class CreateToDoItemRequest : IRequest<Response<Guid>>
{
    public string Title { get; set; }
    public string Note { get; set; }
    public ToDoItemPriority Priority { get; set; }
    public ToDoItemStatus Status { get; set; }
    public DateTime? Deadline { get; set; }
    public Guid ProjectId { get; set; }
    public Guid OwnerId { get; set; }
}