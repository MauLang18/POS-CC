using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.DocumentType.Commands.CreateCommand;
using POS.Application.UseCases.DocumentType.Commands.DeleteCommand;
using POS.Application.UseCases.DocumentType.Commands.UpdateCommand;
using POS.Application.UseCases.DocumentType.Queries.GetAllQuery;
using POS.Application.UseCases.DocumentType.Queries.GetByIdQuery;
using POS.Application.UseCases.DocumentType.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> DocumentTypeList([FromQuery] GetAllDocumentTypeQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> DocumentTypeSelect()
    {
        var response = await _mediator.Send(new GetSelectDocumentTypeQuery());
        return Ok(response);
    }

    [HttpGet("{documentTypeId:int}")]
    public async Task<IActionResult> DocumentTypeById(int documentTypeId)
    {
        var response = await _mediator.Send(new GetDocumentTypeByIdQuery() { DocumentTypeId = documentTypeId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> DocumentTypeCreate([FromBody] CreateDocumentTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> DocumentTypeUpdate([FromBody] UpdateDocumentTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{documentTypeId:int}")]
    public async Task<IActionResult> DocumentTypeDelete(int documentTypeId)
    {
        var response = await _mediator.Send(new DeleteDocumentTypeCommand() { DocumentTypeId = documentTypeId });
        return Ok(response);
    }
}