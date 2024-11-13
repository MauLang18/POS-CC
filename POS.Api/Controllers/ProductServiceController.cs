using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.ProductService.Commands.CreateCommand;
using POS.Application.UseCases.ProductService.Commands.DeleteCommand;
using POS.Application.UseCases.ProductService.Commands.UpdateCommand;
using POS.Application.UseCases.ProductService.Queries.GetAllQuery;
using POS.Application.UseCases.ProductService.Queries.GetByIdQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductServiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ProductServiceList([FromQuery] GetAllProductServiceQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{productServiceId:int}")]
    public async Task<IActionResult> ProductServiceById(int productServiceId)
    {
        var response = await _mediator.Send(new GetProductServiceByIdQuery() { ProductServiceId = productServiceId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> ProductServiceCreate([FromForm] CreateProductServiceCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> ProductServiceUpdate([FromForm] UpdateProductServiceCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{productServiceId:int}")]
    public async Task<IActionResult> ProductServiceDelete(int productServiceId)
    {
        var response = await _mediator.Send(new DeleteProductServiceCommand() { ProductServiceId = productServiceId });
        return Ok(response);
    }
}