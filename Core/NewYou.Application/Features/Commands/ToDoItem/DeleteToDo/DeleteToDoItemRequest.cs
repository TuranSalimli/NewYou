using MediatR;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Commands.ToDoItem.DeleteToDo;

public class DeleteToDoItemRequest : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
    public DeleteToDoItemRequest(Guid id)
    {
        Id = id;
    }
    public DeleteToDoItemRequest()
    {
    }
}