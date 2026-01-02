using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewYou.Application.Features.Commands.ToDoItem.CreateToDo;
using NewYou.Application.Features.Commands.ToDoItem.DeleteToDo;
using NewYou.Application.Features.Commands.ToDoItem.UpdateToDo;
using NewYou.Application.Features.Queries.ToDoItem.GetAllToDoItems;
using NewYou.Application.Features.Queries.ToDoItem.GetByIdToDoItem;

namespace NewYou.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateToDoItemRequest request)
    {
        var response = await _mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet] 
    public async Task<IActionResult> GetAll() 
    {
     
        var response = await _mediator.Send(new GetAllToDoItemsRequest());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetByIdToDoItemRequest { Id = id });

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateToDoItemRequest request)
    {
        var response = await _mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteToDoItemRequest(id);

        var response = await _mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }
}