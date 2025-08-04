using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;

namespace TravelbilityApp.WebAPI.Filters
{
    public class NotAdmin : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.IsAdmin() == true)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
