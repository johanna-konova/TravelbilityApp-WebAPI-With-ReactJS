using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;

using TravelbilityApp.Core.Contracts;

namespace TravelbilityApp.WebAPI.Filters
{
    public class PropertyPublisherAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("id", out var rowId) == false ||
                rowId is not Guid propertyId)
            {
                context.Result = new BadRequestResult();
                return;
            }

            IPropertyService? propertyService =
                context.HttpContext.RequestServices.GetService<IPropertyService>();

            if (propertyService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            var userId = context.HttpContext.User.Id();

            var isUserPropertyPublisher = await propertyService.IsUserPropertyPublisherAsync(propertyId, userId);

            if (isUserPropertyPublisher == false)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}