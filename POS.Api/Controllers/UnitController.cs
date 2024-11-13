using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Unit.Commands.CreateCommand;
using POS.Application.UseCases.Unit.Commands.DeleteCommand;
using POS.Application.UseCases.Unit.Commands.UpdateCommand;
using POS.Application.UseCases.Unit.Queries.GetAllQuery;
using POS.Application.UseCases.Unit.Queries.GetByIdQuery;
using POS.Application.UseCases.Unit.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnitController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> UnitList([FromQuery] GetAllUnitQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> UnitSelect()
    {
        var response = await _mediator.Send(new GetSelectUnitQuery());
        return Ok(response);
    }

    [HttpGet("{unitId:int}")]
    public async Task<IActionResult> UnitById(int unitId)
    {
        var response = await _mediator.Send(new GetUnitByIdQuery() { UnitId = unitId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> UnitCreate([FromBody] CreateUnitCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UnitUpdate([FromBody] UpdateUnitCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{unitId:int}")]
    public async Task<IActionResult> UnitDelete(int unitId)
    {
        var response = await _mediator.Send(new DeleteUnitCommand() { UnitId = unitId });
        return Ok(response);
    }
}