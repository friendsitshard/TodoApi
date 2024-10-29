using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoAPI.Models;

namespace TodoAPI.Filters.ActionFilters
{
    public class TodoPostValidateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var todo = context.ActionArguments["todo"] as TodoTask;
            if(todo == null)
            {
                context.ModelState.AddModelError("Todo task", "Your todo task is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
