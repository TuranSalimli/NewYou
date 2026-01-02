using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewYou.Application.Features.Commands.Project.CreateProject;
using NewYou.Application.Features.Commands.Project.DeleteProject;
using NewYou.Application.Features.Commands.Project.UpdateProject;
using NewYou.Application.Features.Commands.ToDoItem.UpdateToDo;
using NewYou.Application.Features.Queries.Project.GetAllProjects;
using NewYou.Application.Features.Queries.Project.GetProjectById;
using NewYou.Application.Features.Queries.ToDoItem.GetAllToDoItems;
// Digər query və command namespace-ləri bura gələcək

namespace NewYou.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        var response = await _mediator.Send(new GetAllProjectsRequest());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProjectRequest request)
    {
        var response = await _mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteProjectRequest(id));
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetByIdProjectRequest(id));
        return StatusCode(response.StatusCode, response);
    }
}