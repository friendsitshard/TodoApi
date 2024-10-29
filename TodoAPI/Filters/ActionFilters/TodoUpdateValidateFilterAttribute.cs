using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoAPI.Models;

namespace TodoAPI.Filters.ActionFilters
{
    public class TodoUpdateValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            var todoTask = context.ActionArguments as TodoTask;
            if (todoTask != null && id.HasValue && id != todoTask.Id)
            {
                context.ModelState.AddModelError("ID", "Your id and todo id doesn't match");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
