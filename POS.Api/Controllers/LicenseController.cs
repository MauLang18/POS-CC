using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.License.Commands.CreateCommand;
using POS.Application.UseCases.License.Commands.DeleteCommand;
using POS.Application.UseCases.License.Commands.UpdateCommand;
using POS.Application.UseCases.License.Queries.GetAllQuery;
using POS.Application.UseCases.License.Queries.GetByIdQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LicenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public LicenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> LicenseList([FromQuery] GetAllLicenseQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{licenseId:int}")]
    public async Task<IActionResult> LicenseById(int licenseId)
    {
        var response = await _mediator.Send(new GetLicenseByIdQuery() { LicenseId = licenseId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> LicenseCreate([FromBody] CreateLicenseCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> LicenseUpdate([FromBody] UpdateLicenseCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{licenseId:int}")]
    public async Task<IActionResult> LicenseDelete(int licenseId)
    {
        var response = await _mediator.Send(new DeleteLicenseCommand() { LicenseId = licenseId });
        return Ok(response);
    }
}