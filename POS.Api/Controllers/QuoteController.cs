using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Quote.Commands.CreateCommand;
using POS.Application.UseCases.Quote.Commands.DeleteCommand;
using POS.Application.UseCases.Quote.Commands.UpdateCommand;
using POS.Application.UseCases.Quote.Queries.GetAllQuery;
using POS.Application.UseCases.Quote.Queries.GetByIdQuery;
using POS.Application.UseCases.Quote.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuoteController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuoteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> QuoteList([FromQuery] GetAllQuoteQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> QuoteSelect()
    {
        var response = await _mediator.Send(new GetSelectQuoteQuery());
        return Ok(response);
    }

    [HttpGet("{quoteId:int}")]
    public async Task<IActionResult> QuoteById(int quoteId)
    {
        var response = await _mediator.Send(new GetQuoteByIdQuery() { QuoteId = quoteId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> QuoteCreate([FromBody] CreateQuoteCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> QuoteUpdate([FromBody] UpdateQuoteCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{quoteId:int}")]
    public async Task<IActionResult> QuoteDelete(int quoteId)
    {
        var response = await _mediator.Send(new DeleteQuoteCommand() { QuoteId = quoteId });
        return Ok(response);
    }
}