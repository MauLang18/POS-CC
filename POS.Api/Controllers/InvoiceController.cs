using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Invoice.Commands.CreateCommand;
using POS.Application.UseCases.Invoice.Commands.DeleteCommand;
using POS.Application.UseCases.Invoice.Queries.GetAllQuery;
using POS.Application.UseCases.Invoice.Queries.GetByIdQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> InvoiceList([FromQuery] GetAllInvoiceQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{invoiceId:int}")]
    public async Task<IActionResult> InvoiceById(int invoiceId)
    {
        var response = await _mediator.Send(new GetInvoiceByIdQuery() { InvoiceId = invoiceId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> InvoiceCreate([FromBody] CreateInvoiceCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{invoiceId:int}")]
    public async Task<IActionResult> InvoiceDelete(int invoiceId)
    {
        var response = await _mediator.Send(new DeleteInvoiceCommand() { InvoiceId = invoiceId });
        return Ok(response);
    }
}