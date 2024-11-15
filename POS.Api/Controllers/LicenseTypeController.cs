using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.LicenseType.Commands.CreateCommand;
using POS.Application.UseCases.LicenseType.Commands.DeleteCommand;
using POS.Application.UseCases.LicenseType.Commands.UpdateCommand;
using POS.Application.UseCases.LicenseType.Queries.GetAllQuery;
using POS.Application.UseCases.LicenseType.Queries.GetByIdQuery;
using POS.Application.UseCases.LicenseType.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LicenseTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LicenseTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> LicenseTypeList([FromQuery] GetAllLicenseTypeQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> LicenseTypeSelect()
    {
        var response = await _mediator.Send(new GetSelectLicenseTypeQuery());
        return Ok(response);
    }

    [HttpGet("{licenseTypeId:int}")]
    public async Task<IActionResult> LicenseTypeById(int licenseTypeId)
    {
        var response = await _mediator.Send(new GetLicenseTypeByIdQuery() { LicenseTypeId = licenseTypeId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> LicenseTypeCreate([FromBody] CreateLicenseTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> LicenseTypeUpdate([FromBody] UpdateLicenseTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{licenseTypeId:int}")]
    public async Task<IActionResult> LicenseTypeDelete(int licenseTypeId)
    {
        var response = await _mediator.Send(new DeleteLicenseTypeCommand() { LicenseTypeId = licenseTypeId });
        return Ok(response);
    }
}