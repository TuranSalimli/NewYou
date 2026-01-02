using MediatR;
using NewYou.Application.DTOs;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Queries.ToDoItem.GetByIdToDoItem;

public class GetByIdToDoItemRequest : IRequest<Response<ToDoItemDTO>>
{
    public Guid Id { get; set; }
}