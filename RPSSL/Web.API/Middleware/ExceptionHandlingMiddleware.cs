using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
            await context.Response.WriteAsync(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code, object errors)
    {
        var result = JsonSerializer.Serialize(new
        {
            errors = errors is string ? new List<string> { errors.ToString() ?? string.Empty } : errors,
            exceptionType = ex.GetType().Name
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(result);
    }
}