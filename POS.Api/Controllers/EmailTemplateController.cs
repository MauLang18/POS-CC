using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.EmailTemplate.Commands.CreateCommand;
using POS.Application.UseCases.EmailTemplate.Commands.DeleteCommand;
using POS.Application.UseCases.EmailTemplate.Commands.UpdateCommand;
using POS.Application.UseCases.EmailTemplate.Queries.GetAllQuery;
using POS.Application.UseCases.EmailTemplate.Queries.GetByIdQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailTemplateController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailTemplateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> EmailTemplateList([FromQuery] GetAllEmailTemplateQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{emailTemplateId:int}")]
    public async Task<IActionResult> EmailTemplateById(int emailTemplateId)
    {
        var response = await _mediator.Send(new GetEmailTemplateByIdQuery() { EmailTemplateId = emailTemplateId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> EmailTemplateCreate([FromBody] CreateEmailTemplateCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> EmailTemplateUpdate([FromBody] UpdateEmailTemplateCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{emailTemplateId:int}")]
    public async Task<IActionResult> EmailTemplateDelete(int emailTemplateId)
    {
        var response = await _mediator.Send(new DeleteEmailTemplateCommand() { EmailTemplateId = emailTemplateId });
        return Ok(response);
    }
}