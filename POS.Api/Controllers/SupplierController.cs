using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Supplier.Commands.CreateCommand;
using POS.Application.UseCases.Supplier.Commands.DeleteCommand;
using POS.Application.UseCases.Supplier.Commands.UpdateCommand;
using POS.Application.UseCases.Supplier.Queries.GetAllQuery;
using POS.Application.UseCases.Supplier.Queries.GetByIdQuery;
using POS.Application.UseCases.Supplier.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> SupplierList([FromQuery] GetAllSupplierQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> SupplierSelect()
    {
        var response = await _mediator.Send(new GetSelectSupplierQuery());
        return Ok(response);
    }

    [HttpGet("{supplierId:int}")]
    public async Task<IActionResult> SupplierById(int supplierId)
    {
        var response = await _mediator.Send(new GetSupplierByIdQuery() { SupplierId = supplierId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> SupplierCreate([FromBody] CreateSupplierCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> SupplierUpdate([FromBody] UpdateSupplierCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{supplierId:int}")]
    public async Task<IActionResult> SupplierDelete(int supplierId)
    {
        var response = await _mediator.Send(new DeleteSupplierCommand() { SupplierId = supplierId });
        return Ok(response);
    }
}