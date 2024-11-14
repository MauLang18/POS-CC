using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.DocumentTemplate.Commands.CreateCommand;
using POS.Application.UseCases.DocumentTemplate.Commands.DeleteCommand;
using POS.Application.UseCases.DocumentTemplate.Commands.UpdateCommand;
using POS.Application.UseCases.DocumentTemplate.Queries.GetAllQuery;
using POS.Application.UseCases.DocumentTemplate.Queries.GetByIdQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentTemplateController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentTemplateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> DocumentTemplateList([FromQuery] GetAllDocumentTemplateQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{documentTemplateId:int}")]
    public async Task<IActionResult> DocumentTemplateById(int documentTemplateId)
    {
        var response = await _mediator.Send(new GetDocumentTemplateByIdQuery() { DocumentTemplateId = documentTemplateId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> DocumentTemplateCreate([FromBody] CreateDocumentTemplateCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> DocumentTemplateUpdate([FromBody] UpdateDocumentTemplateCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{documentTemplateId:int}")]
    public async Task<IActionResult> DocumentTemplateDelete(int documentTemplateId)
    {
        var response = await _mediator.Send(new DeleteDocumentTemplateCommand() { DocumentTemplateId = documentTemplateId });
        return Ok(response);
    }
}