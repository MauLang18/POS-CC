﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Category.Commands.CreateCommand;
using POS.Application.UseCases.Category.Commands.DeleteCommand;
using POS.Application.UseCases.Category.Commands.UpdateCommand;
using POS.Application.UseCases.Category.Queries.GetAllQuery;
using POS.Application.UseCases.Category.Queries.GetByIdQuery;
using POS.Application.UseCases.Category.Queries.GetSelectQuery;

namespace POS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> CategoryList([FromQuery] GetAllCategoryQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> CategorySelect()
    {
        var response = await _mediator.Send(new GetSelectCategoryQuery());
        return Ok(response);
    }

    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> CategoryById(int categoryId)
    {
        var response = await _mediator.Send(new GetCategoryByIdQuery() { CategoryId = categoryId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CategoryCreate([FromBody] CreateCategoryCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> CategoryUpdate([FromBody] UpdateCategoryCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{categoryId:int}")]
    public async Task<IActionResult> CategoryDelete(int categoryId)
    {
        var response = await _mediator.Send(new DeleteCategoryCommand() { CategoryId = categoryId });
        return Ok(response);
    }
}