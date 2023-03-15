using System.Net;
using WingsOn.Api.Models;
using WingsOn.Services.Exceptions;

namespace WingsOn.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, ex.Message);
            await WriteResponse(context, HttpStatusCode.UnprocessableEntity, new ErrorResponse(ex.Message));
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            await WriteResponse(context, HttpStatusCode.NotFound, new ErrorResponse(ex.Message));
        }
        catch (ServiceException ex)
        {
            _logger.LogError(ex, ex.Message);
            await WriteResponse(context, HttpStatusCode.InternalServerError, new ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await WriteResponse(context, HttpStatusCode.InternalServerError, new ErrorResponse("Oops, something went wrong!"));
        }
    }

    private Task WriteResponse(HttpContext context, HttpStatusCode responseCode, ErrorResponse error)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)responseCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(error);
    }
}
