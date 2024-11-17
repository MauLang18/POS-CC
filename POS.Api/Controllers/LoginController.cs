﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.User.Queries.LoginQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}