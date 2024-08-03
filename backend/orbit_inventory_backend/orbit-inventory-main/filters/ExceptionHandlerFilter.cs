using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using orbit_inventory_core.Exception;

namespace orbit_inventory_main.filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        HandleExceptionAsync(context);
    }
    
    private static void HandleExceptionAsync(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            NotFoundException _ => new NotFoundObjectResult(""),
            UnauthorizedAccessException => new UnauthorizedObjectResult(""),
            BadRequestException exception => new BadRequestObjectResult(exception.Messages),
            _ => new ObjectResult(null) { StatusCode = (int?)HttpStatusCode.InternalServerError }
        };
    }
}