
using Microsoft.AspNetCore.Http;
using System.Net;

namespace VisaApplicationSharedUI.Controller.ExceptionHandler;
public class ExceptionHandling : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, (int)HttpStatusCode.NotFound, e.Message);
        }

        catch (InternalServerException e)
        {
            await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, e.Message);
        }

        catch (BadRequestException e)
        {
            await HandleExceptionAsync(context, (int)HttpStatusCode.BadRequest, e.Message);
        }

        catch (UnAuthorizedException e)
        {
            await HandleExceptionAsync(context, (int)HttpStatusCode.Unauthorized, e.Message);
        }

        catch (Exception e)
        {
            await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private Task HandleExceptionAsync(
            HttpContext context, int httpStatusCode, string message)
    {
        context.Response.StatusCode = httpStatusCode;

        return context.Response.WriteAsync(new ErrorDetails
        {
            statusCode = httpStatusCode,
            errorMessage = message
        }.ToString());
    }
}
