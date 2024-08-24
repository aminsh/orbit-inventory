using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using orbit_inventory_core.request;

namespace orbit_inventory_main.filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
            context.Result = new BadRequestObjectResult(context.ModelState.ToErrorMessage());
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}