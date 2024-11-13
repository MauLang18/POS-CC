using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.VoucherType.Commands.CreateCommand;
using POS.Application.UseCases.VoucherType.Commands.DeleteCommand;
using POS.Application.UseCases.VoucherType.Commands.UpdateCommand;
using POS.Application.UseCases.VoucherType.Queries.GetAllQuery;
using POS.Application.UseCases.VoucherType.Queries.GetByIdQuery;
using POS.Application.UseCases.VoucherType.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoucherTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public VoucherTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> VoucherTypeList([FromQuery] GetAllVoucherTypeQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> VoucherTypeSelect()
    {
        var response = await _mediator.Send(new GetSelectVoucherTypeQuery());
        return Ok(response);
    }

    [HttpGet("{voucherTypeId:int}")]
    public async Task<IActionResult> VoucherTypeById(int voucherTypeId)
    {
        var response = await _mediator.Send(new GetVoucherTypeByIdQuery() { VoucherTypeId = voucherTypeId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> VoucherTypeCreate([FromBody] CreateVoucherTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> VoucherTypeUpdate([FromBody] UpdateVoucherTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{voucherTypeId:int}")]
    public async Task<IActionResult> VoucherTypeDelete(int voucherTypeId)
    {
        var response = await _mediator.Send(new DeleteVoucherTypeCommand() { VoucherTypeId = voucherTypeId });
        return Ok(response);
    }
}