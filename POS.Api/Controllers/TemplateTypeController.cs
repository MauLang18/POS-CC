using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.TemplateType.Commands.CreateCommand;
using POS.Application.UseCases.TemplateType.Commands.DeleteCommand;
using POS.Application.UseCases.TemplateType.Commands.UpdateCommand;
using POS.Application.UseCases.TemplateType.Queries.GetAllQuery;
using POS.Application.UseCases.TemplateType.Queries.GetByIdQuery;
using POS.Application.UseCases.TemplateType.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TemplateTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public TemplateTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> TemplateTypeList([FromQuery] GetAllTemplateTypeQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> TemplateTypeSelect()
    {
        var response = await _mediator.Send(new GetSelectTemplateTypeQuery());
        return Ok(response);
    }

    [HttpGet("{templateTypeId:int}")]
    public async Task<IActionResult> TemplateTypeById(int templateTypeId)
    {
        var response = await _mediator.Send(new GetTemplateTypeByIdQuery() { TemplateTypeId = templateTypeId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> TemplateTypeCreate([FromBody] CreateTemplateTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> TemplateTypeUpdate([FromBody] UpdateTemplateTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{templateTypeId:int}")]
    public async Task<IActionResult> TemplateTypeDelete(int templateTypeId)
    {
        var response = await _mediator.Send(new DeleteTemplateTypeCommand() { TemplateTypeId = templateTypeId });
        return Ok(response);
    }
}