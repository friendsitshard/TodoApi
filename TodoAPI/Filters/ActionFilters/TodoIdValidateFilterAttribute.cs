using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoAPI.Filters.ActionFilters
{
    public class TodoIdValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            if (id.HasValue)
            {
                if (id.Value <= 0)
                {
                    context.ModelState.AddModelError("ID", "ID is invalid");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
        }
    }
}
