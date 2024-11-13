using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.CreditType.Commands.CreateCommand;
using POS.Application.UseCases.CreditType.Commands.DeleteCommand;
using POS.Application.UseCases.CreditType.Commands.UpdateCommand;
using POS.Application.UseCases.CreditType.Queries.GetAllQuery;
using POS.Application.UseCases.CreditType.Queries.GetByIdQuery;
using POS.Application.UseCases.CreditType.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreditTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> CreditTypeList([FromQuery] GetAllCreditTypeQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> CreditTypeSelect()
    {
        var response = await _mediator.Send(new GetSelectCreditTypeQuery());
        return Ok(response);
    }

    [HttpGet("{creditTypeId:int}")]
    public async Task<IActionResult> CreditTypeById(int creditTypeId)
    {
        var response = await _mediator.Send(new GetCreditTypeByIdQuery() { CreditTypeId = creditTypeId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreditTypeCreate([FromBody] CreateCreditTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> CreditTypeUpdate([FromBody] UpdateCreditTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{creditTypeId:int}")]
    public async Task<IActionResult> CreditTypeDelete(int creditTypeId)
    {
        var response = await _mediator.Send(new DeleteCreditTypeCommand() { CreditTypeId = creditTypeId });
        return Ok(response);
    }
}