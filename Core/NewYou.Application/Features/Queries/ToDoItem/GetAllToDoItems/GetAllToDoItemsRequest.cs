using MediatR;
using NewYou.Application.GenericResponses;
using NewYou.Application.DTOs;

namespace NewYou.Application.Features.Queries.ToDoItem.GetAllToDoItems;

public class GetAllToDoItemsRequest : IRequest<Response<IList<ToDoItemDTO>>>
{
}