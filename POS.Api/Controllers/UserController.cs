using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.User.Commands.CreateCommand;
using POS.Application.UseCases.User.Commands.DeleteCommand;
using POS.Application.UseCases.User.Commands.UpdateCommand;
using POS.Application.UseCases.User.Queries.GetAllQuery;
using POS.Application.UseCases.User.Queries.GetByIdQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> UserList([FromQuery] GetAllUserQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> UserById(int userId)
    {
        var response = await _mediator.Send(new GetUserByIdQuery() { UserId = userId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> UserCreate([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UserUpdate([FromBody] UpdateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{userId:int}")]
    public async Task<IActionResult> UserDelete(int userId)
    {
        var response = await _mediator.Send(new DeleteUserCommand() { UserId = userId });
        return Ok(response);
    }
}