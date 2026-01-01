using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewYou.Application.Features.Commands.ToDoItem.CreateToDo;

namespace NewYou.API.Controllers;

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
}