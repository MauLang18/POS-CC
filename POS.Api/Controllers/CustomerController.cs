using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Customer.Commands.CreateCommand;
using POS.Application.UseCases.Customer.Commands.DeleteCommand;
using POS.Application.UseCases.Customer.Commands.UpdateCommand;
using POS.Application.UseCases.Customer.Queries.GetAllQuery;
using POS.Application.UseCases.Customer.Queries.GetByIdQuery;
using POS.Application.UseCases.Customer.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> CustomerList([FromQuery] GetAllCustomerQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> CustomerSelect()
    {
        var response = await _mediator.Send(new GetSelectCustomerQuery());
        return Ok(response);
    }

    [HttpGet("{customerId:int}")]
    public async Task<IActionResult> CustomerById(int customerId)
    {
        var response = await _mediator.Send(new GetCustomerByIdQuery() { CustomerId = customerId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CustomerCreate([FromBody] CreateCustomerCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> CustomerUpdate([FromBody] UpdateCustomerCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{customerId:int}")]
    public async Task<IActionResult> CustomerDelete(int customerId)
    {
        var response = await _mediator.Send(new DeleteCustomerCommand() { CustomerId = customerId });
        return Ok(response);
    }
}