using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Sale.Commands.CreateCommand;
using POS.Application.UseCases.Sale.Commands.DeleteCommand;
using POS.Application.UseCases.Sale.Commands.UpdateCommand;
using POS.Application.UseCases.Sale.Queries.GetAllQuery;
using POS.Application.UseCases.Sale.Queries.GetByIdQuery;
using POS.Application.UseCases.Sale.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> SaleList([FromQuery] GetAllSaleQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> SaleSelect()
    {
        var response = await _mediator.Send(new GetSelectSaleQuery());
        return Ok(response);
    }

    [HttpGet("{saleId:int}")]
    public async Task<IActionResult> SaleById(int saleId)
    {
        var response = await _mediator.Send(new GetSaleByIdQuery() { SaleId = saleId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> SaleCreate([FromBody] CreateSaleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> SaleUpdate([FromBody] UpdateSaleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{saleId:int}")]
    public async Task<IActionResult> SaleDelete(int saleId)
    {
        var response = await _mediator.Send(new DeleteSaleCommand() { SaleId = saleId });
        return Ok(response);
    }
}