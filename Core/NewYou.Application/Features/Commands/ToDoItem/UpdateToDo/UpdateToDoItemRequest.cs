using MediatR;
using NewYou.Application.GenericResponses; 
using NewYou.Domain.Enums;

namespace NewYou.Application.Features.Commands.ToDoItem.UpdateToDo;

public class UpdateToDoItemRequest : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public ToDoItemPriority Priority { get; set; }
    public ToDoItemStatus Status { get; set; }
    public DateTime? Deadline { get; set; }
}