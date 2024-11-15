using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Purchase.Commands.CreateCommand;
using POS.Application.UseCases.Purchase.Commands.DeleteCommand;
using POS.Application.UseCases.Purchase.Queries.GetAllQuery;
using POS.Application.UseCases.Purchase.Queries.GetByIdQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public PurchaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> PurchaseList([FromQuery] GetAllPurchaseQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{purchaseId:int}")]
    public async Task<IActionResult> PurchaseById(int purchaseId)
    {
        var response = await _mediator.Send(new GetPurchaseByIdQuery() { PurchaseId = purchaseId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PurchaseCreate([FromBody] CreatePurchaseCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{purchaseId:int}")]
    public async Task<IActionResult> PurchaseDelete(int purchaseId)
    {
        var response = await _mediator.Send(new DeletePurchaseCommand() { PurchaseId = purchaseId });
        return Ok(response);
    }
}