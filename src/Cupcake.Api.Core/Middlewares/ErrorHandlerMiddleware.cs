using Cupcake.Api.Core.Models;
using Cupcake.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Cupcake.Api.Core.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        // TODO add logger
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var apiResponse = new ApiResponse<string>() { Success = false };

        int statusCode = (int)HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case ValidationException:
                apiResponse.errors = ((BaseException)exception).Errors;
                break;
            case KeyNotFoundException:
            case NotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                apiResponse.errors = ((BaseException)exception).Errors;
                break;
            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                break;
            default:
                break;
        }

        response.StatusCode = statusCode;

        return response.WriteAsync(JsonSerializer.Serialize(apiResponse));
    }
}
