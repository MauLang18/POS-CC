using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Status.Commands.CreateCommand;
using POS.Application.UseCases.Status.Commands.DeleteCommand;
using POS.Application.UseCases.Status.Commands.UpdateCommand;
using POS.Application.UseCases.Status.Queries.GetAllQuery;
using POS.Application.UseCases.Status.Queries.GetByIdQuery;
using POS.Application.UseCases.Status.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> StatusList([FromQuery] GetAllStatusQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> StatusSelect()
    {
        var response = await _mediator.Send(new GetSelectStatusQuery());
        return Ok(response);
    }

    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> StatusById(int categoryId)
    {
        var response = await _mediator.Send(new GetStatusByIdQuery() { StatusId = categoryId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> StatusCreate([FromBody] CreateStatusCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> StatusUpdate([FromBody] UpdateStatusCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{categoryId:int}")]
    public async Task<IActionResult> StatusDelete(int categoryId)
    {
        var response = await _mediator.Send(new DeleteStatusCommand() { StatusId = categoryId });
        return Ok(response);
    }
}