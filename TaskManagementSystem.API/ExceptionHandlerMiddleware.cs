using System.Net;
using System.Text.Json;
using TaskManagementSystem.Domain.Errors;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.API;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (EntityNotFoundException ex)
        {
            await WriteResponse(context, HttpStatusCode.BadRequest, ex);
        }
        catch (DomainException ex)
        {
            await WriteResponse(context, HttpStatusCode.BadRequest, ex);
        }
        catch (Exception ex)
        {
            await WriteResponse(context, HttpStatusCode.InternalServerError, ex);
        }
    }

    private Task WriteResponse(HttpContext context, HttpStatusCode statusCode, Exception exception)
    {
        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var traceId = context.TraceIdentifier;
        
        var title = IsServerInternalError() ? null : exception.Message;
        
        ErrorResult errorResult;

        // the logic of error handling will be extended following the extension of different exception types
        if (exception is DomainException)
        {
            errorResult = new ErrorResult
            {
                Title = title,
                StatusCode = (int)statusCode,
                Path = context.Request.Path,
                TraceId = traceId
            };
        }
        else
        {
            errorResult = new ErrorResult
            {
                StatusCode = (int)statusCode,
                Path = context.Request.Path,
                TraceId = traceId
            };
        }

        var jsonContent = JsonSerializer.Serialize(errorResult);
        return context.Response.WriteAsync(jsonContent);
        
        bool IsServerInternalError()
        {
            return (int)statusCode >= 500 && (int)statusCode <= 599;
        }
    }
}