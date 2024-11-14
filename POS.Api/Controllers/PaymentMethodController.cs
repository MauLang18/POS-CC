using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.PaymentMethod.Commands.CreateCommand;
using POS.Application.UseCases.PaymentMethod.Commands.DeleteCommand;
using POS.Application.UseCases.PaymentMethod.Commands.UpdateCommand;
using POS.Application.UseCases.PaymentMethod.Queries.GetAllQuery;
using POS.Application.UseCases.PaymentMethod.Queries.GetByIdQuery;
using POS.Application.UseCases.PaymentMethod.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentMethodController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> PaymentMethodList([FromQuery] GetAllPaymentMethodQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> PaymentMethodSelect()
    {
        var response = await _mediator.Send(new GetSelectPaymentMethodQuery());
        return Ok(response);
    }

    [HttpGet("{paymentMethodId:int}")]
    public async Task<IActionResult> PaymentMethodById(int paymentMethodId)
    {
        var response = await _mediator.Send(new GetPaymentMethodByIdQuery() { PaymentMethodId = paymentMethodId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PaymentMethodCreate([FromBody] CreatePaymentMethodCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> PaymentMethodUpdate([FromBody] UpdatePaymentMethodCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{paymentMethodId:int}")]
    public async Task<IActionResult> PaymentMethodDelete(int paymentMethodId)
    {
        var response = await _mediator.Send(new DeletePaymentMethodCommand() { PaymentMethodId = paymentMethodId });
        return Ok(response);
    }
}