using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Project.Commands.CreateCommand;
using POS.Application.UseCases.Project.Commands.DeleteCommand;
using POS.Application.UseCases.Project.Commands.UpdateCommand;
using POS.Application.UseCases.Project.Queries.GetAllQuery;
using POS.Application.UseCases.Project.Queries.GetByIdQuery;
using POS.Application.UseCases.Project.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ProjectList([FromQuery] GetAllProjectQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> ProjectSelect()
    {
        var response = await _mediator.Send(new GetSelectProjectQuery());
        return Ok(response);
    }

    [HttpGet("{projectId:int}")]
    public async Task<IActionResult> ProjectById(int projectId)
    {
        var response = await _mediator.Send(new GetProjectByIdQuery() { ProjectId = projectId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> ProjectCreate([FromBody] CreateProjectCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> ProjectUpdate([FromBody] UpdateProjectCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{projectId:int}")]
    public async Task<IActionResult> ProjectDelete(int projectId)
    {
        var response = await _mediator.Send(new DeleteProjectCommand() { ProjectId = projectId });
        return Ok(response);
    }
}