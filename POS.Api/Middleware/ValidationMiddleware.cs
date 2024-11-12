using POS.Application.Commons.Bases;
using POS.Application.Commons.Exceptions;
using POS.Utilities.Static;
using System.Text.Json;

namespace POS.Api.Middleware;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object>
            {
                IsSuccess = false,
                Message = ReplyMessage.MESSAGE_VALIDATE,
                Errors = ex.Errors
            });
        }
    }
}